using UnityEngine;

public class UiUpdate : MonoBehaviour
{
    private GameObject scrollButtons;
    private GameObject volumePanel;
    private SceneData sceneData;
    private CreateMesh createMesh;


    void Start()
    {
        scrollButtons = GameObject.Find("BottomScroll");
        volumePanel = GameObject.Find("VolumePanel");
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        scrollButtons.SetActive(false);
        volumePanel.SetActive(false);
    }

    /* summary :
    * Activates/Deactivates the bottom menu and the top volume panel (UI) if the mesh isn't created
    */
    void Update()
    {
        if(sceneData.IsMeshCreated()){
            scrollButtons.SetActive(true);
            volumePanel.SetActive(true);
            createMesh.UpdateVolumeText();
        }
        else
        {
            scrollButtons.SetActive(false);
            volumePanel.SetActive(false);
        }
    }
}
