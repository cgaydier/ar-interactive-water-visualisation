using UnityEngine;
using UnityEngine.UI;

public class UiUpdate : MonoBehaviour
{
    private GameObject scrollButtons;
    private GameObject volumePanel;
    private SceneData sceneData;
    private CreateMesh createMesh;

    private Sprite chartsOn;
    private Sprite chartsOff;
    private Sprite createOn;
    private Sprite createOff;
    private Sprite legendOn;
    private Sprite legendOff;
    private Sprite settingsOn;
    private Sprite settingsOff;

    void Start()
    {
        scrollButtons = GameObject.Find("BottomScroll");
        volumePanel = GameObject.Find("VolumePanel");
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        scrollButtons.SetActive(false);
        volumePanel.SetActive(false);

        chartsOn = Resources.LoadAll<Sprite>("charts_click")[0];
        chartsOff = Resources.LoadAll<Sprite>("charts")[0];
        createOn = Resources.LoadAll<Sprite>("create_click")[0];
        createOff = Resources.LoadAll<Sprite>("create")[0];
        legendOn = Resources.LoadAll<Sprite>("legend_click")[0];
        legendOff = Resources.LoadAll<Sprite>("legend")[0];
        settingsOn = Resources.LoadAll<Sprite>("settings_click")[0];
        settingsOff = Resources.LoadAll<Sprite>("settings")[0];
    }

    /* summary :
    * Activates/Deactivates the bottom menu and the top volume panel (UI) if the mesh isn't created
    */
    void Update()
    {
        if (sceneData.IsMeshCreated()){
            scrollButtons.SetActive(true);
            volumePanel.SetActive(true);
            createMesh.UpdateVolumeText();
        }
        else
        {
            scrollButtons.SetActive(false);
            volumePanel.SetActive(false);
        }
        
        // Checking the sprites
        if (GameObject.Find("ShowConsumptionButton"))
        {
            if (sceneData.IsLinesShowned() && GameObject.Find("ShowConsumptionButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != chartsOn)
                GameObject.Find("ShowConsumptionButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = chartsOn;
            else if (!sceneData.IsLinesShowned() && GameObject.Find("ShowConsumptionButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != chartsOff)
                GameObject.Find("ShowConsumptionButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = chartsOff;
        }

        if (GameObject.Find("CreateButton"))
        {
            if (sceneData.GetEnumState().currentState == EnumState.State.PlacePoints && GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != createOn)
                GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = createOn;
            else if (sceneData.GetEnumState().currentState != EnumState.State.PlacePoints && GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != createOff)
                GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = createOff;
        }

        if (GameObject.Find("LegendPanel") && GameObject.Find("LegendButton"))
        {
            if (GameObject.Find("LegendPanel").activeSelf && GameObject.Find("LegendButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != legendOn)
                GameObject.Find("LegendButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = legendOn;
            else if (!GameObject.Find("LegendPanel").activeSelf && GameObject.Find("LegendButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != legendOff)
                GameObject.Find("LegendButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = legendOff;
        }
        else if (GameObject.Find("LegendButton"))
            GameObject.Find("LegendButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = legendOff;

        if (GameObject.Find("Settings") && GameObject.Find("OpenCloseSettingsButton"))
        {
            if (GameObject.Find("Settings").activeSelf && GameObject.Find("OpenCloseSettingsButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != settingsOn)
                GameObject.Find("OpenCloseSettingsButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = settingsOn;
            else if (!GameObject.Find("Settings").activeSelf && GameObject.Find("OpenCloseSettingsButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != settingsOff)
                GameObject.Find("OpenCloseSettingsButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = settingsOff;
        }
        else if (GameObject.Find("OpenCloseSettingsButton"))
            GameObject.Find("OpenCloseSettingsButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = settingsOff;
    }
}
