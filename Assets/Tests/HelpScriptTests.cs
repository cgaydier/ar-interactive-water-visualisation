using NUnit.Framework;
using UnityEngine;

public class HelpScriptTests
{
    private HelpScript helpScript;
    private GameObject currentPage;
    private GameObject button;
    private bool isPanelActive;

    public void StartFunction()
    {
        helpScript = GameObject.Find("TipsPanel").GetComponent<HelpScript>();
        button = helpScript.tipsButton;
        button.SetActive(true); 
        helpScript.Start();
        helpScript.StartSceneData();
    }

    [Test]
    public void OpenAndClosePanelTest()
    {
        StartFunction();
        Assert.IsTrue(helpScript != null);
        isPanelActive = helpScript.GetIsPanelActive();
        Assert.IsTrue(isPanelActive == true);

        helpScript.OpenAndClosePanel();

        if (!helpScript)
        {
            Assert.IsTrue(helpScript == null);
        }

        helpScript.OpenAndClosePanel();
        
        Assert.IsTrue(helpScript.tipsButton.activeSelf == false);
        Assert.IsTrue(helpScript.menu.activeSelf == false);
        Assert.IsTrue(helpScript.uIPanel.activeSelf == true);
    }

    [Test]
    public void NextPageTest()
    {
        StartFunction();

        helpScript.NextPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page2, currentPage);

        helpScript.NextPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page3, currentPage);

        helpScript.NextPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page4, currentPage);

        helpScript.NextPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page5, currentPage);

        helpScript.NextPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page6, currentPage);
    }

    [Test]
    public void PrevPageTest()
    {
        StartFunction();

        helpScript.SwitchPage(helpScript.page1, helpScript.page6);
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page6, currentPage);

        helpScript.PrevPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page5, currentPage);

        helpScript.PrevPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page4, currentPage);

        helpScript.PrevPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page3, currentPage);

        helpScript.PrevPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page2, currentPage);

        helpScript.PrevPage();
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page1, currentPage);
    }

    [Test]
    public void SwitchPageTest()
    {
        StartFunction();

        helpScript.SwitchPage(helpScript.page1, helpScript.page2);
        currentPage = helpScript.GetCurrentPage();

        Assert.AreEqual(helpScript.page2, currentPage);
        Assert.IsTrue(helpScript.page1.activeSelf == false);
        Assert.IsTrue(helpScript.page2.activeSelf == true);
    }

    [Test]
    public void CurrentButtonVisibleTest()
    {
        StartFunction();

        helpScript.CurrentButtonVisible();

        Assert.IsTrue(helpScript.prevButton.activeSelf == false);
        Assert.IsTrue(helpScript.nextButton.activeSelf == true);

        helpScript.NextPage();

        helpScript.CurrentButtonVisible();

        Assert.IsTrue(helpScript.prevButton.activeSelf == true);
        Assert.IsTrue(helpScript.nextButton.activeSelf == true);

        helpScript.SwitchPage(helpScript.page1, helpScript.page5);
        currentPage = helpScript.GetCurrentPage();
        Assert.AreEqual(helpScript.page5, currentPage);
        helpScript.NextPage();

        helpScript.CurrentButtonVisible();

        Assert.IsTrue(helpScript.prevButton.activeSelf == true);
        Assert.IsTrue(helpScript.nextButton.activeSelf == false);
    }
}
