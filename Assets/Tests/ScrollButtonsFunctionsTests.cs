using NUnit.Framework;
using UnityEngine;

public class ScrollButtonsFunctionsTests
{
    private ScrollButtonFunctions scrollButtonFunctions;
    private SceneData sceneData;

    private void StartFunction()
    {
        scrollButtonFunctions = GameObject.Find("Content").GetComponent<ScrollButtonFunctions>();
        scrollButtonFunctions.Start();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        sceneData.Start();
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
        Assert.IsFalse(sceneData.IsLinesShowned());
        scrollButtonFunctions.ShowConsumption();
        Assert.IsTrue(sceneData.IsLinesShowned());
        Assert.IsTrue(scrollButtonFunctions.legendPanel.activeSelf);

        scrollButtonFunctions.ShowConsumption();
        Assert.IsFalse(sceneData.IsLinesShowned());
        Assert.IsFalse(scrollButtonFunctions.legendPanel.activeSelf);

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
