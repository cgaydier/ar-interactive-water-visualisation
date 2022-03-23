using UnityEngine;

/* summary : 
 * Linked to UICanvas/BottomScroll/Viewport/Content
 * Handles the scroll buttons
 * 
 * variables :
 * - public - 
 * legendPanel - Legend GameObject
 * examplePanel - AverageConso GameObject
 * settings - Settings GameObject
 * menuPanel - MenuPanel GameObject
 * 
 * - private -
 * sceneData - Link to SceneData's script
 * createLine - Link to LineHandler's script
 */
public class ScrollButtonFunctions : MonoBehaviour
{
    public GameObject legendPanel;
    public GameObject examplePanel;
    public GameObject settings;
    public MenuScript menuPanel;
    private SceneData sceneData;
    private CreateLine createLine;


    public void Start()
    {
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        createLine = GameObject.Find("LineHandler").GetComponent<CreateLine>();
        settings.SetActive(true);
        GameObject.Find("Settings").GetComponent<SettingsFunctions>().Start();
        settings.SetActive(false);
    }

    /* summary :
     * Called on touch of the OpenCloseSettingButton
     * Changes visibility of the settings panel
     */
    public void OpenCloseSettings()
    {
        if (settings != null)
        {
            if(menuPanel.GetIsActive())
                menuPanel.OpenAndCloseMenu();
            bool isActive = settings.activeSelf;
            settings.SetActive(!isActive);
            if (!isActive)
            {
                settings.GetComponent<SettingsFunctions>().RefreshAll();
                if (GameObject.Find("AverageConso"))
                    GameObject.Find("AverageConso").SetActive(false);
            }
            sceneData.GetEnumState().ChangeSettingScene();
        }
    }

    /* summary :
     * Called on touch of the ShowConsumptionButton
     * Displays lines for the different settings data.
     * Calculates the thickness for every line and calls createLine
     */
    public void ShowConsumption()
    {
        if (!sceneData.IsLinesShowned())
        {
            foreach (int i in System.Enum.GetValues(typeof(SceneData.DataName)))
            {
                SceneData.DataName dataName = (SceneData.DataName)i;
                float cptData = sceneData.GetDataCpt(dataName);
                if (cptData > 0)
                {
                    float tmp = sceneData.GetDataConsumption(dataName);
                    switch (sceneData.GetCurrentTime())
                    {
                        case SceneData.TimeName.Day:
                            tmp /= 7f;
                            break;
                        case SceneData.TimeName.Week:
                            break;
                        case SceneData.TimeName.Month:
                            tmp *= 4;
                            break;
                        case SceneData.TimeName.Year:
                            tmp *= 52;
                            break;
                        default:
                            Debug.Log("Unknown Time type !" + sceneData.GetCurrentTime());
                            break;
                    }
                    createLine.AddLine((tmp / sceneData.GetSurfaceMesh() / sceneData.GetScale()) * cptData,
                                       sceneData.GetDataColor(dataName),
                                       sceneData.GetVertices());
                }
            }
            sceneData.SetLinesShowned(true);
            legendPanel.SetActive(true);
        }
        else
        {
            createLine.ClearAll();
            sceneData.SetLinesShowned(false);
            legendPanel.SetActive(false);
        }
    }

    /* summary :
     * Resets the lines to show the consumption
     */
    public void ResetConsumption()
    {
        if (sceneData.IsLinesShowned())
        {
            ShowConsumption();
            ShowConsumption();
        }
    }

    /* summary :
    * Gets the state of settings (active or not)
    */
    public bool GetIsActive()
    {
        return settings.activeSelf;
    }

    /* summary :
     * Changes visibility of the example panel
     */
    public void OpenCloseExamplePanel()
    {
        examplePanel.SetActive(!examplePanel.activeSelf);
        if (examplePanel.activeSelf && GameObject.Find("Settings"))
            GameObject.Find("Settings").SetActive(false);
    }
}
