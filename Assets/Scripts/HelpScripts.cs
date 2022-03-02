using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScripts : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject Page_1;
    public GameObject Page_2;
    public GameObject CurrentPage;

    bool isPanelActive = true;

    public void OpenAndClosePanel()
    {
        if (isPanelActive == false) {
            UIPanel.SetActive(!isPanelActive);
            isPanelActive = true;
        }else{
            UIPanel.SetActive(!isPanelActive);
            isPanelActive = false;
        }
    }

    public void ChangePage()
    {
        if(CurrentPage == Page_1){
            Page_1.SetActive(false);
            Page_2.SetActive(true);
        }
    }
}
