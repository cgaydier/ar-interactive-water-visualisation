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
}
