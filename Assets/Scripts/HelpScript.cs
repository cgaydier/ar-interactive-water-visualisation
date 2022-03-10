using UnityEngine;

public class HelpScript : MonoBehaviour
{
    public SceneDatas sceneDatas;
    public GameObject UIPanel;
    public GameObject Page_1, Page_2, Page_3, Page_4, Page_5, Page_6;
    public GameObject CurrentPage;
    public GameObject Menu;
    public GameObject PrevButton, NextButton, TipsButton;

    bool isPanelActive = true;

    /* summary :
    * permits to open and close the Tutorial panel
    */
    public void OpenAndClosePanel()
    {
        if (isPanelActive == false)
        {
            CurrentButtonVisible(CurrentPage);

            TipsButton.SetActive(false);
            Menu.SetActive(false);
            UIPanel.SetActive(!isPanelActive);
            isPanelActive = true;

            sceneDatas.enumState.SetTuto();
        }
        else
        {
            UIPanel.SetActive(!isPanelActive);
            TipsButton.SetActive(true);
            Menu.SetActive(true);
            isPanelActive = false;

            sceneDatas.enumState.SetMainScene();
        }
    }

    /* summary :
    * permits to switch from current page to the next one
    */
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
        }

        else if(CurrentPage == Page_5)
        {
            SwitchPage(Page_5, Page_6);
            NextButton.SetActive(false);
        }
    }

    /* summary :
    * permits to switch from current page to the previous one
    */
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
            SwitchPage(Page_5, Page_4);
        }

        else if(CurrentPage == Page_6)
        {
            Page_6.SetActive(true);
            SwitchPage(Page_6, Page_5);
        }
    }

    /* summary :
    * permits to change the current page, hide the previous one et show 
    * the next one
    *   
    * param :
    * PrevPage - page to hide
    * NextPage - page to show
    */
    public void SwitchPage(GameObject PrevPage, GameObject NextPage)
    {
        PrevPage.SetActive(false);
        NextPage.SetActive(true);
        CurrentPage = NextPage;
    }


    /* summary :
    * permits to show the previous and next buttons only if it's 
    * necessary at the press of the tips button
    *
    * param :
    * CurrentPage - active page
    */
    public void CurrentButtonVisible(GameObject CurrentPage)
    {
        if (CurrentPage == Page_1)
        {
            PrevButton.SetActive(false);
            NextButton.SetActive(true);
        }
            
        else if (CurrentPage == Page_6)
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
