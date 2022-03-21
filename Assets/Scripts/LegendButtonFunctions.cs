using UnityEngine;
using UnityEngine.UI;

/* summary :
 * Linked to UICanvas/TopRightPanel/Legend/LegendPanel
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
    public GameObject SubMenu;

    private SceneData sceneData;
    private bool first = true;
    private bool isActive = false;

    public void Start()
    {
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        if (first)
        {
            foreach (SceneData.DataName name in SceneData.DataName.GetValues(typeof(SceneData.DataName)))
            {
                GameObject.Find(name.ToString()).GetComponent<Image>().color = sceneData.GetDataColor(name);
            }
            first = false;
        }
    }

    /* summary :
    * Opens and closes the Legend
    */
    public void OpenAndCloseLegend()
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

    public bool GetIsActive()
    {
        return isActive;
    }
}
