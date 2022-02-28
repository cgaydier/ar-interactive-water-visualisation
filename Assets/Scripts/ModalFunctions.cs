using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalFunctions : MonoBehaviour
{
    float waterCpt;
    int counter = 0;

    WaterConsumption waterConsumption;
    
    Text scoreText;

    void Start()
    {
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
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().AddWater(waterCpt);
        counter++;
        RefreshText();
    }

    public void RemoveConsumption()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().RemoveWater(waterCpt);
        counter = ((counter - 1) > 0) ? counter - 1 : 0;
        RefreshText();
    }

    private void RefreshText()
    {
        scoreText.text = counter.ToString();
    }
}