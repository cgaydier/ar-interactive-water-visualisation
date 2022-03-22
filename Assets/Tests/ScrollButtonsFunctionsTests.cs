using NUnit.Framework;
using UnityEngine;

public class ScrollButtonsFunctionsTests
{
    private ScrollButtonFunctions scrollButtonFunctions;

    private void StartFunction()
    {
        scrollButtonFunctions = GameObject.Find("Content").GetComponent<ScrollButtonFunctions>();
        scrollButtonFunctions.Start();
    }

    [Test]
    public void OpenCloseSettingsTest()
    {
        StartFunction();

        Assert.IsTrue(!scrollButtonFunctions.settings.activeSelf);
        scrollButtonFunctions.OpenCloseSettings();
        Assert.IsTrue(scrollButtonFunctions.settings.activeSelf);
        scrollButtonFunctions.OpenCloseSettings();
        Assert.IsTrue(!scrollButtonFunctions.settings.activeSelf);
    }

    [Test]
    public void ShowConsumptionTest()
    {
    }

    [Test]
    public void ResetConsumptionTest()
    {
    }

    [Test]
    public void GetIsActiveTest()
    {
        StartFunction();
        Assert.AreEqual(scrollButtonFunctions.GetIsActive(), scrollButtonFunctions.settings.activeSelf);
    }

    [Test]
    public void OpenCloseExamplePanelTest()
    {
        StartFunction();

        Assert.IsTrue(!scrollButtonFunctions.examplePanel.activeSelf);
        scrollButtonFunctions.OpenCloseExamplePanel();
        Assert.IsTrue(scrollButtonFunctions.examplePanel.activeSelf);
        scrollButtonFunctions.OpenCloseExamplePanel();
        Assert.IsTrue(!scrollButtonFunctions.examplePanel.activeSelf);
    }
}
