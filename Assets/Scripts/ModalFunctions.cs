using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalFunctions : MonoBehaviour
{
    public Text scoreText;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
    }

    void Update()
    {
        scoreText.text = counter.ToString();
    }

    public void AddBath()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().AddWater(0.5f);
        counter++;
    }

    public void RemoveBath()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().RemoveWater(0.5f);
        counter = ((counter - 1) > 0) ? counter - 1 : 0;
    }

    public void AddShower()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().AddWater(0.3f);
        counter++;
    }

    public void RemoveShower()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().RemoveWater(0.3f);
        counter = ((counter - 1) > 0) ? counter - 1 : 0;
    }

    public void AddHandWashingDishes()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().AddWater(0.25f);
        counter++;
    }

    public void RemoveHandWashingDishes()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().RemoveWater(0.25f);
        counter = ((counter - 1) > 0) ? counter - 1 : 0;
    }

    public void AddDishWasher()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().AddWater(0.15f);
        counter++;
    }

    public void RemoveDishWasher()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().RemoveWater(0.15f);
        counter = ((counter - 1) > 0) ? counter - 1 : 0;
    }

    public void AddWashingMachine()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().AddWater(0.4f);
        counter++;
    }

    public void RemoveWashingMachine()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().RemoveWater(0.4f);
        counter = ((counter - 1) > 0) ? counter - 1 : 0;
    }
}
