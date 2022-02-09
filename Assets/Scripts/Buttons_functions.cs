using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons_functions : MonoBehaviour
{
    public GameObject objectToShow;

    void Start()
    {
        objectToShow.SetActive(false);
    }

    public void DisplayCube()
    {
        if (objectToShow.activeSelf)
        {
            objectToShow.SetActive(false);
        }
        else
        {
            objectToShow.SetActive(true);
        }

    }
}
