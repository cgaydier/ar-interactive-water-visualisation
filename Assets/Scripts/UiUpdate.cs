using UnityEngine;
using UnityEngine.UI;

/* summary : 
 * Linked to UiCanvas
 * Handles the changing sprites and volume panel text
 * 
 * variables :
 * - private - 
 * scrollButtons - BottomScroll GameObject
 * volumePanel - VolumePanel GameObject
 * sceneData - Link to SceneData's script
 * createMesh - Link to MeshHandler's script
 * chartsOn - Sprite for the chart button when used
 * chartsOff - Sprite for the chart button when not used
 * createOn - Sprite for the create button when used
 * createOff - Sprite for the create button when not used
 * legendOn - Sprite for the legend button when used
 * legendOff - Sprite for the legend button when not used
 * settingsOn - Sprite for the settings button when used
 * settingsOff - Sprite for the settings button when not used
 * homeOn - Sprite for the home button when used
 * homeOff - Sprite for the home button when not used
 * menuOn - Sprite for the home button when used
 * menuOff - Sprite for the home button when not used
 */

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
    private Sprite homeOn;
    private Sprite homeOff;
    private Sprite menuOn;
    private Sprite menuOff;

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
        homeOn = Resources.LoadAll<Sprite>("home_click")[0];
        homeOff = Resources.LoadAll<Sprite>("home")[0];
        menuOn = Resources.LoadAll<Sprite>("menu_click")[0];
        menuOff = Resources.LoadAll<Sprite>("menu")[0];
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

        if (GameObject.Find("AverageConso") && GameObject.Find("HomeRefButton"))
        {
            if (GameObject.Find("AverageConso").activeSelf && GameObject.Find("HomeRefButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != homeOn)
                GameObject.Find("HomeRefButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = homeOn;
            else if (!GameObject.Find("AverageConso").activeSelf && GameObject.Find("HomeRefButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != homeOff)
                GameObject.Find("HomeRefButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = homeOff;
        }
        else if (GameObject.Find("HomeRefButton"))
            GameObject.Find("HomeRefButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = homeOff;

        if (GameObject.Find("MenuButton") && GameObject.Find("SubMenuPanel"))
        {
            if (GameObject.Find("SubMenuPanel").activeSelf && GameObject.Find("MenuButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != menuOn)
                GameObject.Find("MenuButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = menuOn;
            else if (!GameObject.Find("SubMenuPanel").activeSelf && GameObject.Find("MenuButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite != menuOff)
                GameObject.Find("MenuButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = menuOff;
        }
        else if (GameObject.Find("MenuButton"))
            GameObject.Find("MenuButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = menuOff;
    }
}
