using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScripts : MonoBehaviour
{
    public SceneDatas sceneDatas;
    public GameObject UIPanel;
    public GameObject Page_1, Page_2, Page_3, Page_4, Page_5;
    public GameObject CurrentPage;
    public GameObject Menu;
    public GameObject PrevButton, NextButton, TipsButton;

    bool isPanelActive = true;

    public void OpenAndClosePanel()
    {
        if (isPanelActive == false)
        {
            CurrentButtonVisible(CurrentPage);

            TipsButton.SetActive(false);
            UIPanel.SetActive(!isPanelActive);
            isPanelActive = true;

            sceneDatas.enumState.SetTuto();
        }
        else
        {
            UIPanel.SetActive(!isPanelActive);
            TipsButton.SetActive(true);
            isPanelActive = false;

            sceneDatas.enumState.SetMainScene();
        }
    }

    public void NextPage()
    {
        if(CurrentPage == Page_1)
        {  
            SwitchPage(Page_1, Page_2);
            PrevButton.SetActive(true);
        }

        else if(CurrentPage == Page_2)
        {
            SwitchPage(Page_2, Page_3);
        }

        else if(CurrentPage == Page_3)
        {
            SwitchPage(Page_3, Page_4);
        }

        else if(CurrentPage == Page_4)
        {
            SwitchPage(Page_4, Page_5);
            NextButton.SetActive(false);
        }
    }

    public void PrevPage()
    {
        if(CurrentPage == Page_2)
        {
            SwitchPage(Page_2, Page_1);
            PrevButton.SetActive(false);
        }

        else if(CurrentPage == Page_3)
        {
            SwitchPage(Page_3, Page_2);
        }

        else if(CurrentPage == Page_4)
        {
            SwitchPage(Page_4, Page_3);
        }

        else if(CurrentPage == Page_5)
        {
            NextButton.SetActive(true);
            SwitchPage(Page_5, Page_4);
        }
    }

    public void SwitchPage(GameObject PrevPage, GameObject NextPage)
    {
        PrevPage.SetActive(false);
        NextPage.SetActive(true);
        CurrentPage = NextPage;
    }

    public void CurrentButtonVisible(GameObject CurrentPage)
    {
        if (CurrentPage == Page_1)
        {
            PrevButton.SetActive(false);
            NextButton.SetActive(true);
        }
            
        else if (CurrentPage == Page_5)
        {
            PrevButton.SetActive(true);
            NextButton.SetActive(false); 
        }

        else 
        {
            PrevButton.SetActive(true);
            NextButton.SetActive(true); 
        }
    }
}
