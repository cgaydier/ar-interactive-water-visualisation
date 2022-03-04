using UnityEngine;
using UnityEngine.UI;

public class SettingsFunctions : MonoBehaviour
{
    float waterConsumption;
    int counter = 0;
    CreateMesh createMesh;
    SceneDatas sceneDatas;
    
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
                showerBackground = GameObject.Find("Shower").GetComponent<Image>().color = sceneDatas.datasColors[0];
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.datasName.Shower));
                break;
            case "Bath":
                scoreText = GameObject.Find("Bath/Score").GetComponent<Text>();
                bathBackground = GameObject.Find("Bath").GetComponent<Image>().color = sceneDatas.datasColors[1];
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.datasName.Bath));
                break;
            case "HandDish":
                scoreText = GameObject.Find("HandDish/Score").GetComponent<Text>();
                handDishBackground = GameObject.Find("HandDish").GetComponent<Image>().color = sceneDatas.datasColors[2];
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.datasName.HandDish));
                break;
            case "DishWasher":
                scoreText = GameObject.Find("DishWasher/Score").GetComponent<Text>();
                dishWasherBackground = GameObject.Find("DishWasher").GetComponent<Image>().color = sceneDatas.datasColors[3];
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.datasName.DishWasher));
                break;
            case "WashingMachine":
                scoreText = GameObject.Find("WashingMachine/Score").GetComponent<Text>();
                washingMachineBackground = GameObject.Find("WashingMachine").GetComponent<Image>().color = sceneDatas.datasColors[4];
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.datasName.WashingMachine));
                break;
            case "Bathroom":
                scoreText = GameObject.Find("Bathroom/Score").GetComponent<Text>();
                bathroomBackground = GameObject.Find("Bathroom").GetComponent<Image>().color = sceneDatas.datasColors[5];
                waterConsumption = sceneDatas.GetDataConsumption((SceneDatas.datasName.Bathroom));
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
        createMesh.AddWater(waterConsumption);
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
            createMesh.RemoveWater(waterConsumption);
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