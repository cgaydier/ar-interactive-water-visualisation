using UnityEngine;
using UnityEngine.UI;

/* summary :
 * Handle the legend panel
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
}
