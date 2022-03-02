using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScripts : MonoBehaviour
{
    public GameObject UIPanel;
    bool isActive = true;

    public void OpenAndClosePanel()
    {
        if (isActive == false) {
            UIPanel.SetActive(!isActive);
            isActive = true;
        }else
        {
            UIPanel.SetActive(!isActive);
            isActive = false;
        }
    }
}
