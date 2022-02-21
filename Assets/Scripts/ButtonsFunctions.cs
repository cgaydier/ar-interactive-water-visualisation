using UnityEngine;

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
            enumState.SetMainScene();
            objectToShow.SetActive(false);
        }
        else
        {
            enumState.SetDisplayCube();
            objectToShow.SetActive(true);
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
