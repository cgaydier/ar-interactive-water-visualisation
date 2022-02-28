using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsFunctions : MonoBehaviour
{
    float waterCpt;
    int counter = 0;
    WaterConsumption waterConsumption;
    
    Text scoreText;

    void Start()
    {
        waterConsumption = GameObject.Find("WaterConsumptionDatas").GetComponent<WaterConsumption>();
        switch (gameObject.name)
        {
            case "Shower":
                scoreText = GameObject.Find("Shower/Score").GetComponent<Text>();
                waterCpt = waterConsumption.shower;
                break;
            case "Bath":
                scoreText = GameObject.Find("Bath/Score").GetComponent<Text>();
                waterCpt = waterConsumption.bath;
                break;
            case "HandDish":
                scoreText = GameObject.Find("HandDish/Score").GetComponent<Text>();
                waterCpt = waterConsumption.handDish;
                break;
            case "DishWasher":
                scoreText = GameObject.Find("DishWasher/Score").GetComponent<Text>();
                waterCpt = waterConsumption.dishwasher;
                break;
            case "WashingMachine":
                scoreText = GameObject.Find("WashingMachine/Score").GetComponent<Text>();
                waterCpt = waterConsumption.washingMachine;
                break;
            default:
                Debug.Log("Type not known for increment consumption !");
                break;
        }
    }

    public void AddConsumption()
    {
        GameObject.Find("MeshHandler").GetComponent<CreateMesh>().AddWater(waterCpt);
        counter++;
        RefreshText();
    }

    public void RemoveConsumption()
    {
        if (counter > 0)
        {
            GameObject.Find("MeshHandler").GetComponent<CreateMesh>().RemoveWater(waterCpt);
            counter --;
            RefreshText();
        }        
    }

    private void RefreshText()
    {
        scoreText.text = counter.ToString();
    }
}