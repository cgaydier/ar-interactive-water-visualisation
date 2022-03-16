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
            if(scrollButtonFunctions.GetIsActive())
                scrollButtonFunctions.OpenCloseSettings();
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
    * Sets the state of isActive
    *
    * parameter :
    * state - defines if the isActive variable is true or false
    */
    public bool GetIsActive()
    {
        return isActive;
    }
}