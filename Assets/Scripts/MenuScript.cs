using UnityEngine;

/* summary :
 * Linked to MenuPanel
 * Handles the AverageConso panel
 * 
 * variables : 
 * - public -
 * subMenu - SubMenuPanel GameObject
 * 
 * - private - 
 * isActive - Used to know if the menu panel is active
 */
public class MenuScript : MonoBehaviour
{
    public GameObject subMenu;

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