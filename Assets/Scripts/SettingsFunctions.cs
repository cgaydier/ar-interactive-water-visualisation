using UnityEngine;
using UnityEngine.UI;

public class SettingsFunctions : MonoBehaviour
{
    private float waterConsumption;
    private CreateMesh createMesh;
    private SceneDatas sceneDatas;
    
    Text scoreText;
    Color showerBackground, bathBackground, handDishBackground,
        dishWasherBackground, washingMachineBackground, bathroomBackground;

    void Start()
    {
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        switch (gameObject.name)
        {
            case "Shower":
                scoreText = GameObject.Find("Shower/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.Shower));
                showerBackground = GameObject.Find("Shower").GetComponent<Image>().color = sceneDatas.datasColors[0];
                break;
            case "Bath":
                scoreText = GameObject.Find("Bath/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.Bath));
                bathBackground = GameObject.Find("Bath").GetComponent<Image>().color = sceneDatas.datasColors[1];
                break;
            case "HandDish":
                scoreText = GameObject.Find("HandDish/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.HandDish));
                handDishBackground = GameObject.Find("HandDish").GetComponent<Image>().color = sceneDatas.datasColors[2];
                break;
            case "DishWasher":
                scoreText = GameObject.Find("DishWasher/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.DishWasher));
                dishWasherBackground = GameObject.Find("DishWasher").GetComponent<Image>().color = sceneDatas.datasColors[3];
                break;
            case "WashingMachine":
                scoreText = GameObject.Find("WashingMachine/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.WashingMachine));
                washingMachineBackground = GameObject.Find("WashingMachine").GetComponent<Image>().color = sceneDatas.datasColors[4];
                break;
            case "Bathroom":
                scoreText = GameObject.Find("Bathroom/Score").GetComponent<Text>();
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.DataName.Bathroom));
                bathroomBackground = GameObject.Find("Bathroom").GetComponent<Image>().color = sceneDatas.datasColors[5];
                break;
            case "Scale":
                scoreText = GameObject.Find("Scale/Score").GetComponent<Text>();
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
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
        int cpt = 0;
        
        switch (gameObject.name)
        {
            case "Shower":
                cpt = sceneDatas.GetDataCpt(SceneDatas.DataName.Shower);
                break;
            case "Bath":
                cpt = sceneDatas.GetDataCpt(SceneDatas.DataName.Bath);
                break;
            case "HandDish":
                cpt = sceneDatas.GetDataCpt(SceneDatas.DataName.HandDish);
                break;
            case "DishWasher":
                cpt = sceneDatas.GetDataCpt(SceneDatas.DataName.DishWasher);
                break;
            case "WashingMachine":
                cpt = sceneDatas.GetDataCpt(SceneDatas.DataName.WashingMachine);
                break;
            case "Bathroom":
                cpt = sceneDatas.GetDataCpt(SceneDatas.DataName.Bathroom);
                break;
            case "Scale":
                cpt = sceneDatas.GetScale();
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
                break;
        }
        scoreText.text = cpt.ToString();
    }
}