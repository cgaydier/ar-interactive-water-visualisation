using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollButtonFunctions : MonoBehaviour
{
    public GameObject Modal;
    public EnumState enumState;

    public void OpenModal()
    {
        if (Modal != null)
        {
            bool isActive = Modal.activeSelf;
            Modal.SetActive(!isActive);
            enumState.ChangeModalScene();
        }
    }

    public void AddWater()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().AddWater();
    }

    public void RemoveWater()
    {
        GameObject.Find("MenuHandler").GetComponent<CreateMesh>().RemoveWater();
    }
}
