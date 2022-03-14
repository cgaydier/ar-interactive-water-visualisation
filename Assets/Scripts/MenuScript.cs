using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject SubMenu;

    bool isActive = false;

    /* summary :
    * Open and close the Menu
    */
    public void OpenAndCloseMenu()
    {
        if (isActive == false)
        {
            SubMenu.SetActive(true);
            isActive = true;
        }
        else
        {
            SubMenu.SetActive(false);
            isActive = false;
        }
    }
}