using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScripts : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject Page_1, Page_2, Page_3, Page_4;
    public GameObject CurrentPage;
    public GameObject Menu;
    public GameObject PrevButton, NextButton;

    bool isPanelActive = true;

    public void OpenAndClosePanel()
    {
        if (isPanelActive == false) {
            SwitchPage(CurrentPage, Page_1);
            PrevButton.SetActive(false);
            NextButton.SetActive(true);
            Menu.SetActive(false);

            UIPanel.SetActive(!isPanelActive);
            isPanelActive = true;
        }else{
            UIPanel.SetActive(!isPanelActive);
            isPanelActive = false;
            
            Menu.SetActive(true);
        }
    }

    public void NextPage()
    {
        if(CurrentPage == Page_1){  
            SwitchPage(Page_1, Page_2);
            PrevButton.SetActive(true);
        }

        else if(CurrentPage == Page_2){
            SwitchPage(Page_2, Page_3);
        }

        else if(CurrentPage == Page_3){
            SwitchPage(Page_3, Page_4);
            NextButton.SetActive(false);
        }
    }

    public void PrevPage()
    {
        if(CurrentPage == Page_2){
            SwitchPage(Page_2, Page_1);
            PrevButton.SetActive(false);
        }

        else if(CurrentPage == Page_3){
            SwitchPage(Page_3, Page_2);
        }

        else if(CurrentPage == Page_4){
            NextButton.SetActive(true);
            SwitchPage(Page_4, Page_3);
        }
    }

    public void SwitchPage(GameObject PrevPage, GameObject NextPage)
    {
        PrevPage.SetActive(false);
        NextPage.SetActive(true);
        CurrentPage = NextPage;
    }
}