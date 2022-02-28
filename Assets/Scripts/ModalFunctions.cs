using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalFunctions : MonoBehaviour
{
    float waterCpt;
    int counter = 0;
    
    Text scoreText;

    void Start()
    {
        switch (gameObject.name)
        {
            case "Shower":
                scoreText = GameObject.Find("Shower/Score").GetComponent<Text>();
                waterCpt = 0.3f;
                break;
            case "Bath":
                scoreText = GameObject.Find("Bath/Score").GetComponent<Text>();
                waterCpt = 0.5f;
                break;
            case "HandDish":
                scoreText = GameObject.Find("HandDish/Score").GetComponent<Text>();
                waterCpt = 0.25f;
                break;
            case "DishWasher":
                scoreText = GameObject.Find("DishWasher/Score").GetComponent<Text>();
                waterCpt = 0.15f;
                break;
            case "Washing":
                scoreText = GameObject.Find("Washing/Score").GetComponent<Text>();
                waterCpt = 0.4f;
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