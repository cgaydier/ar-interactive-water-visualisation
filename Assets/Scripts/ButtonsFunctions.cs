using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsFunctions : MonoBehaviour
{
    public GameObject objectToShow;
    public EnumState enumState;

    void Start()
    {
        objectToShow.SetActive(false);
    }

    public void DisplayCube()
    {
        if (objectToShow.activeSelf)
        {
            enumState.setMainScene();
            objectToShow.SetActive(false);
        }
        else
        {
            enumState.setDisplayCube();
            objectToShow.SetActive(true);
        }

    }
}
