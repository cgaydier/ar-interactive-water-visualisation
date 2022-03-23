using UnityEngine;
using UnityEngine.UI;

/* summary :
 * Linked to UICanvas/TipsPanel
 * Handles the tuto
 * 
 * variables :
 * - public -
 * uIPanel - TipsPanel GameObject
 * page1 - IntroPanel GameObject
 * page 2 - QuitPanel GameObject
 * page 3 - MenuPanel GameObject
 * page 4 - Menu2Panel GameObject
 * page 5 - Menu3Panel GameObject
 * page 6 - CreditPanel GameObject
 * menu - MenuPanel GameObject
 * prevButton - PrevButton GameObject
 * nextButton - NextButton GameObject
 * tipsButton - TipsButton GameObject
 * createButton - CreateButton GameObject
 * 
 * - private -
 * currentPage - Hold the last page opened
 * sceneData - Link to SceneData's script
 * errorHandler - Link to ErrorHandler's script
 * isPanelActive - Used to know if the panel is active
 */
public class HelpScript : MonoBehaviour
{
    public GameObject uIPanel;
    public GameObject page1, page2, page3, page4, page5, page6;
    public GameObject menu;
    public GameObject prevButton, nextButton, tipsButton;
    public GameObject createButton;

    private GameObject currentPage;
    private SceneData sceneData;
    private ErrorHandler errorHandler;
    private bool isPanelActive;


    public void Start()
    {
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        
        currentPage = page1;
        isPanelActive = true;
    }    
    
    /* summary :
    * Opens and close the Tutorial panel
    */
    public void OpenAndClosePanel()
    {
        if (!isPanelActive)
        {
            CurrentButtonVisible();
            errorHandler.ErrorMessageReset();
            tipsButton.SetActive(false);
            menu.SetActive(false);
            uIPanel.SetActive(!isPanelActive);
            isPanelActive = true;

            sceneData.GetEnumState().SetTuto();
        }
        else
        {
            errorHandler.ErrorMessageReset();
            uIPanel.SetActive(!isPanelActive);
            tipsButton.SetActive(true);
            menu.SetActive(true);
            isPanelActive = false;
            sceneData.GetEnumState().SetMainScene();
            createButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("create")[0];
        }
    }

    /* summary :
    * Switches from current page to the next one
    */
    public void NextPage()
    {
        if (currentPage == page1)
        {  
            SwitchPage(page1, page2);
            prevButton.SetActive(true);
        }

        else if (currentPage == page2)
        {
            SwitchPage(page2, page3);
        }

        else if (currentPage == page3)
        {
            SwitchPage(page3, page4);
        }

        else if (currentPage == page4)
        {
           SwitchPage(page4, page5);
        }

        else if (currentPage == page5)
        {
            SwitchPage(page5, page6);
            nextButton.SetActive(false);
        }
    }

    /* summary :
    * Switches from current page to the previous one
    */
    public void PrevPage()
    {
        if (currentPage == page2)
        {
            SwitchPage(page2, page1);
            prevButton.SetActive(false);
        }

        else if (currentPage == page3)
        {
            SwitchPage(page3, page2);
        }

        else if (currentPage == page4)
        {
            SwitchPage(page4, page3);
        }

        else if (currentPage == page5)
        {
            SwitchPage(page5, page4);
        }

        else if (currentPage == page6)
        {
            SwitchPage(page6, page5);
            nextButton.SetActive(true);
        }
    }

    /* summary :
    * Changes the current page, hide the previous one et show 
    * the next one
    *   
    * param :
    * prevPage - page to hide
    * nextPage - page to show
    */
    public void SwitchPage(GameObject PrevPage, GameObject NextPage)
    {
        PrevPage.SetActive(false);
        NextPage.SetActive(true);
        currentPage = NextPage;
    }

    /* summary :
    * Showes the previous and next buttons only if it's 
    * necessary at the press of the tips button
    */
    public void CurrentButtonVisible()
    {
        if (currentPage == page1)
        {
            prevButton.SetActive(false);
            nextButton.SetActive(true);
        }
            
        else if (currentPage == page6)
        {
            prevButton.SetActive(true);
            nextButton.SetActive(false); 
        }

        else 
        {
            prevButton.SetActive(true);
            nextButton.SetActive(true); 
        }
    }


    /* summary :
    * Gets the current page
    */
    public GameObject GetCurrentPage()
    {
        return currentPage;
    }

    /* summary :
    * Gets the state of isPanelActive
    */
    public bool GetIsPanelActive()
    {
        return isPanelActive;
    }

    /*
     * Test purposes
     */
    public void StartSceneData()
    {
        sceneData.Start();
    }
}
