using UnityEngine;

public class ScrollButtonFunctions : MonoBehaviour
{
    public GameObject settings;
    private EnumState enumState;
    private SceneDatas sceneDatas;
    private CreateLine createLine;

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
        if (!sceneDatas.IsLinesShowned())
        {
            foreach (int i in System.Enum.GetValues(typeof(SceneDatas.DataName)))
            {
                SceneDatas.DataName dataName = (SceneDatas.DataName)i;
                float cptData = sceneDatas.GetDataCpt(dataName);
                if (cptData > 0)
                {
                    createLine.AddLine((sceneDatas.GetDataConsumption(dataName) / sceneDatas.GetSurfaceMesh() / sceneDatas.GetScale()) * cptData,
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
}
