using UnityEngine;

public class ScrollButtonFunctions : MonoBehaviour
{
    public GameObject settings;
    private SceneData sceneData;
    private CreateLine createLine;

    private void Start()
    {
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        createLine = GameObject.Find("LineHandler").GetComponent<CreateLine>();
        settings.SetActive(true);
        GameObject.Find("Settings").GetComponent<SettingsFunctions>().Start();
        settings.SetActive(false);
    }

    /* summary :
     * Called on touch of the OpenCloseSettingButton
     * Change visibility of the setting panel
     */
    public void OpenCloseSettings()
    {
        if (settings != null)
        {
            bool isActive = settings.activeSelf;
            settings.SetActive(!isActive);
            if (!isActive)
                settings.GetComponent<SettingsFunctions>().RefreshAll();
            sceneData.enumState.ChangeSettingScene();
        }
    }

    /* summary :
     * Called on touch of the ShowConsumptionButton
     * Display lines for the different setting's data.
     * Calculate the thickness for every line and call createLine
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
                    switch (sceneData.currentTime)
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
                            Debug.Log("Unknown Time type !" + sceneData.currentTime);
                            break;
                    }
                    createLine.AddLine((tmp / sceneData.GetSurfaceMesh() / sceneData.GetScale()) * cptData,
                                       sceneData.GetDataColor(dataName),
                                       sceneData.GetVertices());
                }
            }
            sceneData.SetLinesShowned(true);
        }
        else
        {
            createLine.ClearAll();
            sceneData.SetLinesShowned(false);
        }
    }

    /* summary :
     * Reset the lines to show the consumption
     */
    public void ResetConsumption()
    {
        if (sceneData.IsLinesShowned())
        {
            ShowConsumption();
            ShowConsumption();
        }
    }
}
