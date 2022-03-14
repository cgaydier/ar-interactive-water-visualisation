using UnityEngine;

public class ScrollButtonFunctions : MonoBehaviour
{
    public GameObject settings;
    private SceneDatas sceneDatas;
    private CreateLine createLine;

    private void Start()
    {
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        createLine = GameObject.Find("LineHandler").GetComponent<CreateLine>();
        settings.SetActive(true);
        GameObject.Find("Settings").GetComponent<SettingsFunctions>().Start();
        settings.SetActive(false);
    }

    public void OpenSettings()
    {
        if (settings != null)
        {
            bool isActive = settings.activeSelf;
            settings.SetActive(!isActive);
            if (!isActive)
                settings.GetComponent<SettingsFunctions>().RefreshAll();
            sceneDatas.enumState.ChangeSettingScene();
        }
    }

    public void ShowConsumption()
    {
        if (!sceneDatas.IsLinesShowned())
        {
            foreach (int i in System.Enum.GetValues(typeof(SceneDatas.DataName)))
            {
                SceneDatas.DataName dataName = (SceneDatas.DataName)i;
                float cptData = sceneDatas.GetDataCpt(dataName);
                if (cptData > 0)
                {
                    float tmp = sceneDatas.GetDataConsumption(dataName);
                    switch (sceneDatas.currentTime)
                    {
                        case SceneDatas.TimeName.Day:
                            tmp /= 7f;
                            break;
                        case SceneDatas.TimeName.Week:
                            break;
                        case SceneDatas.TimeName.Month:
                            tmp *= 4;
                            break;
                        case SceneDatas.TimeName.Year:
                            tmp *= 52;
                            break;
                        default:
                            Debug.Log("Unknown Time type !" + sceneDatas.currentTime);
                            break;
                    }
                    createLine.AddLine((tmp / sceneDatas.GetSurfaceMesh() / sceneDatas.GetScale()) * cptData,
                                       sceneDatas.GetDataColor(dataName),
                                       sceneDatas.GetVertices());
                }
            }
            sceneDatas.SetLinesShowned(true);
        }
        else
        {
            createLine.ClearAll();
            sceneDatas.SetLinesShowned(false);
        }
    }

    public void ResetConsumption()
    {
        if (sceneDatas.IsLinesShowned())
        {
            ShowConsumption();
            ShowConsumption();
        }
    }
}
