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
        foreach (int i in System.Enum.GetValues(typeof(SceneDatas.datasName)))
        {
            float cptData = sceneDatas.GetData((SceneDatas.datasName)i);
            if (cptData > 0)
            {
                Debug.Log("-----------");
                Debug.Log(sceneDatas.dataConsumption[i]);
                createLine.AddLine(cptData / sceneDatas.surfaceMesh / sceneDatas.GetScale() * sceneDatas.dataConsumption[i],
                                   sceneDatas.datasColors[i],
                                   sceneDatas.vertices);
            }
        }
    }
}
