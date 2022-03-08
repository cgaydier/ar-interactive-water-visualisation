using UnityEngine;

public class UiUpdate : MonoBehaviour
{
    private GameObject scrollButtons;
    private GameObject volumePanel;
    private SceneDatas sceneData;


    void Start()
    {
        scrollButtons = GameObject.Find("BottomScroll");
        volumePanel = GameObject.Find("VolumePanel");
        sceneData = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        scrollButtons.SetActive(false);
        volumePanel.SetActive(false);
    }
    void Update()
    {
        if(sceneData.IsMeshCreated()){
            scrollButtons.SetActive(true);
            volumePanel.SetActive(true);
        }
        else
        {
            scrollButtons.SetActive(false);
            volumePanel.SetActive(false);
        }
    }
}
