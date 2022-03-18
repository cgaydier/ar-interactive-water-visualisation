using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject subMenu;
    public ScrollButtonFunctions scrollButtonFunctions;

    private bool isActive = false;

    /* summary :
    * Opens and closes the Menu
    */
    public void OpenAndCloseMenu()
    {
        if (isActive == false)
        {
            if (GameObject.Find("AverageConso"))
                GameObject.Find("AverageConso").SetActive(false);
            if (GameObject.Find("Settings"))
                GameObject.Find("Settings").SetActive(false);

            subMenu.SetActive(true);
            isActive = true;
        }
        else
        {
            subMenu.SetActive(false);
            isActive = false;
        }
    }

    /* summary :
    * Gets the state of isActive
    */
    public bool GetIsActive()
    {
        return isActive;
    }
}