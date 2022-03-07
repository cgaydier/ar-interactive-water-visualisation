using UnityEngine;
using UnityEngine.UI;

public class SettingsFunctions : MonoBehaviour
{
    private float waterConsumption;
    private CreateMesh createMesh;
    private SceneDatas sceneDatas;
    private Text scoreText;

    void Start()
    {
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        switch (gameObject.name)
        {
            case "Shower":
                scoreText = GameObject.Find("Shower/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.Shower));
                GameObject.Find("Shower").GetComponent<Image>().color = sceneDatas.GetDataColor(SceneDatas.DataName.Shower);
                break;
            case "Bath":
                scoreText = GameObject.Find("Bath/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.Bath));
                GameObject.Find("Bath").GetComponent<Image>().color = sceneDatas.GetDataColor(SceneDatas.DataName.Bath);
                break;
            case "HandDish":
                scoreText = GameObject.Find("HandDish/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.HandDish));
                GameObject.Find("HandDish").GetComponent<Image>().color = sceneDatas.GetDataColor(SceneDatas.DataName.HandDish);
                break;
            case "DishWasher":
                scoreText = GameObject.Find("DishWasher/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.DishWasher));
                GameObject.Find("DishWasher").GetComponent<Image>().color = sceneDatas.GetDataColor(SceneDatas.DataName.DishWasher);
                break;
            case "WashingMachine":
                scoreText = GameObject.Find("WashingMachine/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.WashingMachine));
                GameObject.Find("WashingMachine").GetComponent<Image>().color = sceneDatas.GetDataColor(SceneDatas.DataName.WashingMachine);
                break;
            case "Bathroom":
                scoreText = GameObject.Find("Bathroom/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.Bathroom));
                GameObject.Find("Bathroom").GetComponent<Image>().color = sceneDatas.GetDataColor(SceneDatas.DataName.Bathroom);
                break;
            case "Scale":
                scoreText = GameObject.Find("Scale/Score").GetComponent<Text>();
                break;
            case "TemporalScale":
                scoreText = GameObject.Find("TemporalScale/Score").GetComponent<Text>();
                break;
            default:
                Debug.Log("Unknown Type !" + gameObject.name);
                break;
        }
        RefreshText();
    }

    public void AddConsumption()
    {
        switch (gameObject.name)
        {
            case "Shower":
                sceneDatas.IncrDataCpt(SceneDatas.DataName.Shower);
                break;
            case "Bath":
                sceneDatas.IncrDataCpt(SceneDatas.DataName.Bath);
                break;
            case "HandDish":
                sceneDatas.IncrDataCpt(SceneDatas.DataName.HandDish);
                break;
            case "DishWasher":
                sceneDatas.IncrDataCpt(SceneDatas.DataName.DishWasher);
                break;
            case "WashingMachine":
                sceneDatas.IncrDataCpt(SceneDatas.DataName.WashingMachine);
                break;
            case "Bathroom":
                sceneDatas.IncrDataCpt(SceneDatas.DataName.Bathroom);
                break;
            case "Scale":
                sceneDatas.IncrScale();
                break;
            default:
                Debug.Log("Type not known !");
                break;
        }
        createMesh.AddWater(waterConsumption);
        RefreshText();
    }

    public void RemoveConsumption()
    {
        bool passed = true;
        switch (gameObject.name)
        {
            case "Shower":
                passed = sceneDatas.DecrDataCpt(SceneDatas.DataName.Shower);
                break;
            case "Bath":
                passed = sceneDatas.DecrDataCpt(SceneDatas.DataName.Bath);
                break;
            case "HandDish":
                passed = sceneDatas.DecrDataCpt(SceneDatas.DataName.HandDish);
                break;
            case "DishWasher":
                passed = sceneDatas.DecrDataCpt(SceneDatas.DataName.DishWasher);
                break;
            case "WashingMachine":
                passed = sceneDatas.DecrDataCpt(SceneDatas.DataName.WashingMachine);
                break;
            case "Bathroom":
                passed = sceneDatas.DecrDataCpt(SceneDatas.DataName.Bathroom);
                break;
            case "Scale":
                break;
            default:
                Debug.Log("Type not known !");
                break;
        }
        if (passed)
        {
            createMesh.RemoveWater(waterConsumption);
            RefreshText();
        }
    }

    public void NextTemporalScale()
    {
        switch (sceneDatas.currentTime)
        {
            case SceneDatas.TimeName.Day:
                sceneDatas.currentTime = SceneDatas.TimeName.Week;
                break;
            case SceneDatas.TimeName.Week:
                sceneDatas.currentTime = SceneDatas.TimeName.Month;
                break;
            case SceneDatas.TimeName.Month:
                sceneDatas.currentTime = SceneDatas.TimeName.Year;
                break;
            case SceneDatas.TimeName.Year:
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
                break;
        }
        createMesh.SetWater();
        RefreshText();
    }

    public void PreviousTemporalScale()
    {
        switch (sceneDatas.currentTime)
        {
            case SceneDatas.TimeName.Day:
                break;
            case SceneDatas.TimeName.Week:
                sceneDatas.currentTime = SceneDatas.TimeName.Day;
                break;
            case SceneDatas.TimeName.Month:
                sceneDatas.currentTime = SceneDatas.TimeName.Week;
                break;
            case SceneDatas.TimeName.Year:
                sceneDatas.currentTime = SceneDatas.TimeName.Month;
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
                break;
        }
        createMesh.SetWater();
        RefreshText();
    }

    public void AddScale()
    {
        createMesh.AddScale();
        RefreshText();
    }

    public void RemoveScale()
    {
        if (sceneDatas.DecrScale())
        {
            createMesh.DecrScale();
            RefreshText();
        }
    }

    public void RefreshText()
    {
        string value = "";

        switch (gameObject.name)
        {
            case "Shower":
                value = sceneDatas.GetDataCpt(SceneDatas.DataName.Shower).ToString();
                break;
            case "Bath":
                value = sceneDatas.GetDataCpt(SceneDatas.DataName.Bath).ToString();
                break;
            case "HandDish":
                value = sceneDatas.GetDataCpt(SceneDatas.DataName.HandDish).ToString();
                break;
            case "DishWasher":
                value = sceneDatas.GetDataCpt(SceneDatas.DataName.DishWasher).ToString();
                break;
            case "WashingMachine":
                value = sceneDatas.GetDataCpt(SceneDatas.DataName.WashingMachine).ToString();
                break;
            case "Bathroom":
                value = sceneDatas.GetDataCpt(SceneDatas.DataName.Bathroom).ToString();
                break;
            case "Scale":
                value = sceneDatas.GetScale().ToString();
                break;
            case "TemporalScale":
                value = sceneDatas.currentTime.ToString();
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
                break;
        }
        scoreText.text = value;
    }
}