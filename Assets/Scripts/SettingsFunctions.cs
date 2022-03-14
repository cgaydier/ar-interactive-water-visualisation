using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsFunctions : MonoBehaviour
{
    private CreateMesh createMesh;
    private SceneDatas sceneData;
    private bool first = true;

    public void Start()
    {
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneDatas>();
        if (first)
        {
            foreach (SceneDatas.DataName name in SceneDatas.DataName.GetValues(typeof(SceneDatas.DataName)))
            {
                GameObject.Find(name.ToString()).GetComponent<Image>().color = sceneData.GetDataColor(name);
                GameObject.Find(name.ToString() + "/Text").GetComponent<TextMeshProUGUI>().text += (" (" + sceneData.GetDataConsumption(name) * 1000 + " L)");
            }
            first = false;
        }
    }

    /* summary :
     * Add a consumtion for name parameter.
     * Update mesh with new value
     * Refresh text on setting panel
     * 
     * parameter : 
     * name - name of the data increased
    */

    public void AddConsumption(string name)
    {
        float waterConsumption = 0f;
        foreach (SceneDatas.DataName tmpName in SceneDatas.DataName.GetValues(typeof(SceneDatas.DataName)))
        {
            if (name.Equals(tmpName.ToString()))
            {
                sceneData.IncrDataCpt(tmpName);
                waterConsumption = sceneData.GetDataConsumption(tmpName);
                break;

            }
        }
        createMesh.AddWater(waterConsumption);
        RefreshText(name);
    }

    /* summary :
     * Remove a consumption for name parameter if possible 
     * Update mesh with new value
     * Refresh text on setting panel
     * 
     * parameter :
     * name - name of the data decreased
     */

    public void RemoveConsumption(string name)
    {
        foreach (SceneDatas.DataName tmpName in SceneDatas.DataName.GetValues(typeof(SceneDatas.DataName)))
        {
            if (name.Equals(tmpName.ToString()))
            {
                bool passed = sceneData.DecrDataCpt(tmpName);
                if (passed)
                {
                    float waterConsumption = sceneData.GetDataConsumption(tmpName);
                    createMesh.RemoveWater(waterConsumption);
                    RefreshText(name);
                }
                break;

            }
        }
    }

    /* summary :
     * Change the temporal scale in sceneData for the next one
     * Refresh text on setting panel
     */
    public void NextTemporalScale()
    {
        switch (sceneData.currentTime)
        {
            case SceneDatas.TimeName.Day:
                sceneData.currentTime = SceneDatas.TimeName.Week;
                break;
            case SceneDatas.TimeName.Week:
                sceneData.currentTime = SceneDatas.TimeName.Month;
                break;
            case SceneDatas.TimeName.Month:
                sceneData.currentTime = SceneDatas.TimeName.Year;
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

    /* summary :
     * Change the temporal scale in sceneData for the previous one
     * Refresh text on setting panel
     */
    public void PreviousTemporalScale()
    {
        switch (sceneData.currentTime)
        {
            case SceneDatas.TimeName.Day:
                break;
            case SceneDatas.TimeName.Week:
                sceneData.currentTime = SceneDatas.TimeName.Day;
                break;
            case SceneDatas.TimeName.Month:
                sceneData.currentTime = SceneDatas.TimeName.Week;
                break;
            case SceneDatas.TimeName.Year:
                sceneData.currentTime = SceneDatas.TimeName.Month;
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
                break;
        }
        createMesh.SetWater();
        RefreshText("TemporalScale");
    }

    /* summary :
     * Add one to the current scale
     * Refresh text on setting panel
     */

    public void AddScale()
    {
        createMesh.AddScale();
        RefreshText("Scale");
    }

    /* summary :
     * Remove one to the current scale
     * Refresh text on setting panel
     */
    public void RemoveScale()
    {
        if (sceneData.DecrScale())
        {
            createMesh.DecrScale();
            RefreshText("Scale");
        }
    }

    /* summary :
     * Refresh the name text on the setting panel
     * 
     * parameter :
     * name - name of the data to changed
     */
    public void RefreshText(string name)
    {
        if (name.Equals("Scale"))
        {
            GameObject.Find("Scale/Score").GetComponent<Text>().text = sceneData.GetScale().ToString();
        }
        else if (name.Equals("TemporalScale"))
        {
            GameObject.Find("TemporalScale/Score").GetComponent<Text>().text = sceneData.currentTime.ToString();
        }
        else
        {

            foreach (SceneDatas.DataName tmpName in SceneDatas.DataName.GetValues(typeof(SceneDatas.DataName)))
            {
                if (name.Equals(tmpName.ToString()))
                {
                    GameObject.Find(name+"/Score").GetComponent<Text>().text = sceneData.GetDataCpt(tmpName).ToString();
                    break;

                }
            }
        }
    }

    /* summary :
     * Call RefreshText() for every data's name on the setting panel
     */
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