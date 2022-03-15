using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject SubMenu;

    private bool isActive = false;

    /* summary :
    * Opens and closes the Menu
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