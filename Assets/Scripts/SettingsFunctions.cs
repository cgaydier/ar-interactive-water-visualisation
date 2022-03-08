using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsFunctions : MonoBehaviour
{
    private CreateMesh createMesh;
    private SceneDatas sceneDatas;
    private bool first = true;

    public void Start()
    {
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        if (first)
        {
            foreach (SceneDatas.DataName name in SceneDatas.DataName.GetValues(typeof(SceneDatas.DataName)))
            {
                GameObject.Find(name.ToString()).GetComponent<Image>().color = sceneDatas.GetDataColor(name);
                GameObject.Find(name.ToString() + "/Text").GetComponent<TextMeshProUGUI>().text += (" (" + sceneDatas.GetDataConsumption(name) + " m3)");
            }
            first = false;
        }
    }

    public void AddConsumption(string name)
    {
        float waterConsumption = 0f;
        foreach (SceneDatas.DataName tmpName in SceneDatas.DataName.GetValues(typeof(SceneDatas.DataName)))
        {
            if (name.Equals(tmpName.ToString()))
            {
                sceneDatas.IncrDataCpt(tmpName);
                waterConsumption = sceneDatas.GetDataConsumption(tmpName);
                break;

            }
        }
        createMesh.AddWater(waterConsumption);
        RefreshText(name);
    }

    public void RemoveConsumption(string name)
    {
        foreach (SceneDatas.DataName tmpName in SceneDatas.DataName.GetValues(typeof(SceneDatas.DataName)))
        {
            if (name.Equals(tmpName.ToString()))
            {
                bool passed = sceneDatas.DecrDataCpt(tmpName);
                if (passed)
                {
                    float waterConsumption = sceneDatas.GetDataConsumption(tmpName);
                    createMesh.RemoveWater(waterConsumption);
                    RefreshText(name);
                }
                break;

            }
        }
    }

    public void NextTemporalScale()
    {
        switch (sceneDatas.currentTime)
        {
            case SceneDatas.TimeName.Day:
                sceneDatas.currentTime = SceneDatas.TimeName.Week;
                break;
            case SceneDatas.TimeName.Week:
                sceneDatas.currentTime = SceneDatas.TimeName.Month;
                break;
            case SceneDatas.TimeName.Month:
                sceneDatas.currentTime = SceneDatas.TimeName.Year;
                break;
            case SceneDatas.TimeName.Year:
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
                break;
        }
        createMesh.SetWater();
        RefreshText("TemporalScale");
    }

    public void PreviousTemporalScale()
    {
        switch (sceneDatas.currentTime)
        {
            case SceneDatas.TimeName.Day:
                break;
            case SceneDatas.TimeName.Week:
                sceneDatas.currentTime = SceneDatas.TimeName.Day;
                break;
            case SceneDatas.TimeName.Month:
                sceneDatas.currentTime = SceneDatas.TimeName.Week;
                break;
            case SceneDatas.TimeName.Year:
                sceneDatas.currentTime = SceneDatas.TimeName.Month;
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
                break;
        }
        createMesh.SetWater();
        RefreshText("TemporalScale");
    }

    public void AddScale()
    {
        createMesh.AddScale();
        RefreshText("Scale");
    }

    public void RemoveScale()
    {
        if (sceneDatas.DecrScale())
        {
            createMesh.DecrScale();
            RefreshText("Scale");
        }
    }

    public void RefreshText(string name)
    {
        if (name.Equals("Scale"))
        {
            GameObject.Find("Scale/Score").GetComponent<Text>().text = sceneDatas.GetScale().ToString();
        }
        else if (name.Equals("TemporalScale"))
        {
            GameObject.Find("TemporalScale/Score").GetComponent<Text>().text = sceneDatas.currentTime.ToString();
        }
        else
        {

            foreach (SceneDatas.DataName tmpName in SceneDatas.DataName.GetValues(typeof(SceneDatas.DataName)))
            {
                if (name.Equals(tmpName.ToString()))
                {
                    GameObject.Find(name+"/Score").GetComponent<Text>().text = sceneDatas.GetDataCpt(tmpName).ToString();
                    break;

                }
            }
        }
    }

    public void RefreshAll()
    {
        if (GameObject.Find("Settings"))
        {
            foreach (SceneDatas.DataName tmpName in SceneDatas.DataName.GetValues(typeof(SceneDatas.DataName)))
            {
                RefreshText(tmpName.ToString());
            }
            RefreshText("Scale");
            RefreshText("TemporalScale");
        } 
    }
}