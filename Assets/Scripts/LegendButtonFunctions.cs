using UnityEngine;
using UnityEngine.UI;

/* summary :
 * Linked to UICanvas/TopRightPanel/Legend
 * Handles the legend panel for consumption lines
 * 
 * variable :
 * - public -
 * SubMenu - LegendPanel GameObject
 * 
 * - private -
 * sceneData - Link to SceneData's script
 * first - Used to know if it was the first start called
 * isActive - Used to know if the panel is active
 */
public class LegendButtonFunctions : MonoBehaviour
{
    public GameObject subMenu;

    private SceneData sceneData;
    private bool first = true;
    private bool isActive = false;

    public void Start()
    {
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        if (first)
        {
            subMenu.SetActive(true);
            foreach (SceneData.DataName name in SceneData.DataName.GetValues(typeof(SceneData.DataName)))
            {
                GameObject.Find(name.ToString()).GetComponent<Image>().color = sceneData.GetDataColor(name);
            }
            first = false;
            subMenu.SetActive(false);
        }
    }

    /* summary :
    * Opens and closes the Legend
    */
    public void OpenAndCloseLegend()
    {
        if (isActive == false)
        {
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
