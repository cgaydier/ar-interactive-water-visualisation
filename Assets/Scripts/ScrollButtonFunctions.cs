using UnityEngine;

public class ScrollButtonFunctions : MonoBehaviour
{
    public GameObject settings;
    EnumState enumState;
    SceneDatas sceneDatas;
    CreateLine createLine;

    private void Start()
    {
        enumState = GameObject.Find("SceneState").GetComponent<EnumState>();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        createLine = GameObject.Find("LineHandler").GetComponent<CreateLine>();
    }

    public void OpenModal()
    {
        if (settings != null)
        {
            bool isActive = settings.activeSelf;
            settings.SetActive(!isActive);
            enumState.ChangeModalScene();
        }
    }

    public void ShowConsumption()
    {
        if (!sceneDatas.linesShowned)
        {
            foreach (int i in System.Enum.GetValues(typeof(SceneDatas.datasName)))
            {
                float cptData = sceneDatas.GetDataCpt((SceneDatas.datasName)i);
                if (cptData > 0)
                {
                    createLine.AddLine((sceneDatas.dataConsumption[i] / sceneDatas.surfaceMesh / sceneDatas.GetScale()) * cptData,
                                       sceneDatas.datasColors[i],
                                       sceneDatas.vertices);
                }
            }
            sceneDatas.linesShowned = true;
        }
        else
        {
            createLine.ClearAll();
            sceneDatas.linesShowned = false;
        }
    }
}
