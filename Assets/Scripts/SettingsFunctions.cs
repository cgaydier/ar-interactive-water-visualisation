using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsFunctions : MonoBehaviour
{
    private CreateMesh createMesh;
    private SceneData sceneData;
    private bool first = true;

    public void Start()
    {
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        if (first)
        {
            foreach (SceneData.DataName name in SceneData.DataName.GetValues(typeof(SceneData.DataName)))
            {
                GameObject.Find(name.ToString()).GetComponent<Image>().color = sceneData.GetDataColor(name);
                GameObject.Find(name.ToString() + "/Text").GetComponent<TextMeshProUGUI>().text += (" (" + sceneData.GetDataConsumption(name) * 1000 + " L)");
            }
            first = false;
        }
    }

    /* summary :
     * Adds a consumption for name parameter.
     * Updates mesh with new value
     * Refreshes text on setting panel
     * 
     * parameter : 
     * name - name of the data increased
    */

    public void AddConsumption(string name)
    {
        float waterConsumption = 0f;
        foreach (SceneData.DataName tmpName in SceneData.DataName.GetValues(typeof(SceneData.DataName)))
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
     * Removes a consumption for name parameter if possible 
     * Updates mesh with new value
     * Refreshes text on setting panel
     * 
     * parameter :
     * name - name of the data decreased
     */

    public void RemoveConsumption(string name)
    {
        foreach (SceneData.DataName tmpName in SceneData.DataName.GetValues(typeof(SceneData.DataName)))
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
     * Changes the temporal scale in sceneData for the next one
     * Refreshes text on setting panel
     */
    public void NextTemporalScale()
    {
        switch (sceneData.currentTime)
        {
            case SceneData.TimeName.Day:
                sceneData.currentTime = SceneData.TimeName.Week;
                break;
            case SceneData.TimeName.Week:
                sceneData.currentTime = SceneData.TimeName.Month;
                break;
            case SceneData.TimeName.Month:
                sceneData.currentTime = SceneData.TimeName.Year;
                break;
            case SceneData.TimeName.Year:
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
                break;
        }
        createMesh.SetWater();
        RefreshText("TemporalScale");
    }

    /* summary :
     * Changes the temporal scale in sceneData for the previous one
     * Refreshes text on setting panel
     */
    public void PreviousTemporalScale()
    {
        switch (sceneData.currentTime)
        {
            case SceneData.TimeName.Day:
                break;
            case SceneData.TimeName.Week:
                sceneData.currentTime = SceneData.TimeName.Day;
                break;
            case SceneData.TimeName.Month:
                sceneData.currentTime = SceneData.TimeName.Week;
                break;
            case SceneData.TimeName.Year:
                sceneData.currentTime = SceneData.TimeName.Month;
                break;
            default:
                Debug.Log("Type not known !" + gameObject.name);
                break;
        }
        createMesh.SetWater();
        RefreshText("TemporalScale");
    }

    /* summary :
     * Adds one to the current scale
     * Refreshes text on setting panel
     */

    public void AddScale()
    {
        createMesh.AddScale();
        RefreshText("Scale");
    }

    /* summary :
     * Removes one to the current scale
     * Refreshes text on setting panel
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
     * Refreshes the name text on the setting panel
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

            foreach (SceneData.DataName tmpName in SceneData.DataName.GetValues(typeof(SceneData.DataName)))
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
     * Calls RefreshText() for every data's name on the setting panel
     */
    public void RefreshAll()
    {
        if (GameObject.Find("Settings"))
        {
            foreach (SceneData.DataName tmpName in SceneData.DataName.GetValues(typeof(SceneData.DataName)))
            {
                RefreshText(tmpName.ToString());
            }
            RefreshText("Scale");
            RefreshText("TemporalScale");
        } 
    }
}