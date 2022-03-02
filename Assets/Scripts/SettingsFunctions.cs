using UnityEngine;
using UnityEngine.UI;

public class SettingsFunctions : MonoBehaviour
{
    float waterCpt;
    int counter = 0;
    CreateMesh createMesh;
    SceneDatas sceneDatas;
    
    Text scoreText;

    void Start()
    {
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        switch (gameObject.name)
        {
            case "Shower":
                scoreText = GameObject.Find("Shower/Score").GetComponent<Text>();
                waterCpt = sceneDatas.dataConsumption[sceneDatas.GetData((SceneDatas.datasName)0)];
                break;
            case "Bath":
                scoreText = GameObject.Find("Bath/Score").GetComponent<Text>();
                waterCpt = sceneDatas.dataConsumption[sceneDatas.GetData((SceneDatas.datasName)1)];
                break;
            case "HandDish":
                scoreText = GameObject.Find("HandDish/Score").GetComponent<Text>();
                waterCpt = sceneDatas.dataConsumption[sceneDatas.GetData((SceneDatas.datasName)2)];
                break;
            case "DishWasher":
                scoreText = GameObject.Find("DishWasher/Score").GetComponent<Text>();
                waterCpt = sceneDatas.dataConsumption[sceneDatas.GetData((SceneDatas.datasName)3)];
                break;
            case "WashingMachine":
                scoreText = GameObject.Find("WashingMachine/Score").GetComponent<Text>();
                waterCpt = sceneDatas.dataConsumption[sceneDatas.GetData((SceneDatas.datasName)4)];
                break;
            case "Bathroom":
                scoreText = GameObject.Find("Bathroom/Score").GetComponent<Text>();
                waterCpt = sceneDatas.dataConsumption[sceneDatas.GetData((SceneDatas.datasName)5)];
                break;
            case "Scale":
                counter = 1;
                scoreText = GameObject.Find("Scale/Score").GetComponent<Text>();
                break;
            default:
                Debug.Log("Type not known for increment consumption !" + gameObject.name);
                break;
        }
    }

    public void AddConsumption()
    {
        switch (gameObject.name)
        {
            case "Shower":
                sceneDatas.IncrData(SceneDatas.datasName.Shower);
                break;
            case "Bath":
                sceneDatas.IncrData(SceneDatas.datasName.Bath);
                break;
            case "HandDish":
                sceneDatas.IncrData(SceneDatas.datasName.HandDish);
                break;
            case "DishWasher":
                sceneDatas.IncrData(SceneDatas.datasName.DishWasher);
                break;
            case "WashingMachine":
                sceneDatas.IncrData(SceneDatas.datasName.WashingMachine);
                break;
            case "Bathroom":
                sceneDatas.IncrData(SceneDatas.datasName.Bathroom);
                break;
            case "Scale":
                sceneDatas.IncrScale();
                break;
            default:
                Debug.Log("Type not known for increment consumption !");
                break;
        }

        createMesh.AddWater(waterCpt);
        counter++;
        RefreshText();
    }

    public void RemoveConsumption()
    {
        if (counter > 0)
        {
            switch (gameObject.name)
            {
                case "Shower":
                    sceneDatas.DecrData(SceneDatas.datasName.Shower);
                    break;
                case "Bath":
                    sceneDatas.DecrData(SceneDatas.datasName.Bath);
                    break;
                case "HandDish":
                    sceneDatas.DecrData(SceneDatas.datasName.HandDish);
                    break;
                case "DishWasher":
                    sceneDatas.DecrData(SceneDatas.datasName.DishWasher);
                    break;
                case "WashingMachine":
                    sceneDatas.DecrData(SceneDatas.datasName.WashingMachine);
                    break;
                case "Bathroom":
                    sceneDatas.DecrData(SceneDatas.datasName.Bathroom);
                    break;
                case "Scale":
                    sceneDatas.DecrScale();
                    break;
                default:
                    Debug.Log("Type not known for increment consumption !");
                    break;
            }
            createMesh.RemoveWater(waterCpt);
            counter --;
            RefreshText();
        }        
    }

    public void AddScale()
    {
        createMesh.AddScale();
        counter++;
        RefreshText();
    }

    public void RemoveScale()
    {
        if (counter > 1)
        {
            createMesh.RemoveScale();
            counter--;
            RefreshText();
        }
    }

    private void RefreshText()
    {
        scoreText.text = counter.ToString();
    }
}