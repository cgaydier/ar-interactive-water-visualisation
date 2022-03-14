using UnityEngine;

public class UiUpdate : MonoBehaviour
{
    private GameObject scrollButtons;
    private GameObject volumePanel;
    private SceneDatas sceneData;
    private CreateMesh createMesh;


    void Start()
    {
        scrollButtons = GameObject.Find("BottomScroll");
        volumePanel = GameObject.Find("VolumePanel");
        sceneData = GameObject.Find("SceneData").GetComponent<SceneDatas>();
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        scrollButtons.SetActive(false);
        volumePanel.SetActive(false);
    }

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
