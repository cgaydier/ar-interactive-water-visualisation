using NUnit.Framework;
using UnityEngine;

public class ExampleConsumptionTests
{
    private SceneData sceneData;
    private GameObject examplePanel;
    private ExampleConsumption exampleConsumption;

    private void StartFunction()
    {
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        examplePanel = GameObject.Find("Content").GetComponent<ScrollButtonFunctions>().examplePanel;
        examplePanel.SetActive(true);
        exampleConsumption = GameObject.Find("AverageConso").GetComponent<ExampleConsumption>();
        exampleConsumption.Start();
    }

    [Test]
    public void SetValueTest()
    {
        StartFunction();

        string whichOne;

        whichOne = "Average1P1W";
        exampleConsumption.SetValue(whichOne);

        Assert.IsFalse(examplePanel.activeSelf);
        examplePanel.SetActive(false);
    }
}
