using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class SettingsFunctionsTests
{
    private SettingsFunctions settingsFunctions;
    private SceneData sceneData;
    private void StartFunction()
    {
        settingsFunctions = GameObject.Find("Content").GetComponent<ScrollButtonFunctions>().settings.GetComponent<SettingsFunctions>();
        settingsFunctions.Start();
        GameObject.Find("Content").GetComponent<ScrollButtonFunctions>().settings.SetActive(true);
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        sceneData.Start();
    }

    private void EndFunction()
    {
        sceneData.ClearAll();
        settingsFunctions.RefreshAll();
    }

    [Test]
    public void AddRemoveConsumptionTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bath), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bathroom), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.DishWasher), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.HandDish), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Shower), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.WashingMachine), 0);

        settingsFunctions.AddConsumption("Bath");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bath), 1);
        settingsFunctions.RemoveConsumption("Bath");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bath), 0);

        settingsFunctions.AddConsumption("Bathroom");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bathroom), 1);
        settingsFunctions.RemoveConsumption("Bathroom");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bathroom), 0);

        settingsFunctions.AddConsumption("DishWasher");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.DishWasher), 1);
        settingsFunctions.RemoveConsumption("DishWasher");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.DishWasher), 0);

        settingsFunctions.AddConsumption("HandDish");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.HandDish), 1);
        settingsFunctions.RemoveConsumption("HandDish");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.HandDish), 0);

        settingsFunctions.AddConsumption("Shower");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Shower), 1);
        settingsFunctions.RemoveConsumption("Shower");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Shower), 0);

        settingsFunctions.AddConsumption("WashingMachine");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.WashingMachine), 1);
        settingsFunctions.RemoveConsumption("WashingMachine");
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.WashingMachine), 0);

        EndFunction();
    }

    [Test]
    public void NextPreviousTemporalScaleTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetCurrentTime(), SceneData.TimeName.Week);

        settingsFunctions.PreviousTemporalScale();
        Assert.AreEqual(sceneData.GetCurrentTime(), SceneData.TimeName.Day);

        settingsFunctions.NextTemporalScale();
        Assert.AreEqual(sceneData.GetCurrentTime(), SceneData.TimeName.Week);

        settingsFunctions.NextTemporalScale();
        Assert.AreEqual(sceneData.GetCurrentTime(), SceneData.TimeName.Month);

        settingsFunctions.NextTemporalScale();
        Assert.AreEqual(sceneData.GetCurrentTime(), SceneData.TimeName.Year);

        settingsFunctions.PreviousTemporalScale();
        Assert.AreEqual(sceneData.GetCurrentTime(), SceneData.TimeName.Month);

        settingsFunctions.PreviousTemporalScale();
        Assert.AreEqual(sceneData.GetCurrentTime(), SceneData.TimeName.Week);

        EndFunction();
    }

    [Test]
    public void AddRemoveScaleTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetScale(), 1);

        settingsFunctions.AddScale();
        Assert.AreEqual(sceneData.GetScale(), 2);

        settingsFunctions.RemoveScale();
        Assert.AreEqual(sceneData.GetScale(), 1);

        EndFunction();
    }

    [Test]
    public void RefreshTextTest()
    {
        StartFunction();

        Assert.AreEqual(GameObject.Find("Scale/Score").GetComponent<Text>().text, "1");
        settingsFunctions.AddScale();
        Assert.AreEqual(GameObject.Find("Scale/Score").GetComponent<Text>().text, "2");

        Assert.AreEqual(GameObject.Find("TemporalScale/Score").GetComponent<Text>().text, "Week");
        settingsFunctions.NextTemporalScale();
        Assert.AreEqual(GameObject.Find("TemporalScale/Score").GetComponent<Text>().text, "Month");

        Assert.AreEqual(GameObject.Find("Bath/Score").GetComponent<Text>().text, "0");
        settingsFunctions.AddConsumption("Bath");
        Assert.AreEqual(GameObject.Find("Bath/Score").GetComponent<Text>().text, "1");

        Assert.AreEqual(GameObject.Find("Bathroom/Score").GetComponent<Text>().text, "0");
        settingsFunctions.AddConsumption("Bathroom");
        Assert.AreEqual(GameObject.Find("Bathroom/Score").GetComponent<Text>().text, "1");

        Assert.AreEqual(GameObject.Find("DishWasher/Score").GetComponent<Text>().text, "0");
        settingsFunctions.AddConsumption("DishWasher");
        Assert.AreEqual(GameObject.Find("DishWasher/Score").GetComponent<Text>().text, "1");

        Assert.AreEqual(GameObject.Find("HandDish/Score").GetComponent<Text>().text, "0");
        settingsFunctions.AddConsumption("HandDish");
        Assert.AreEqual(GameObject.Find("HandDish/Score").GetComponent<Text>().text, "1");

        Assert.AreEqual(GameObject.Find("Shower/Score").GetComponent<Text>().text, "0");
        settingsFunctions.AddConsumption("Shower");
        Assert.AreEqual(GameObject.Find("Shower/Score").GetComponent<Text>().text, "1");

        Assert.AreEqual(GameObject.Find("WashingMachine/Score").GetComponent<Text>().text, "0");
        settingsFunctions.AddConsumption("WashingMachine");
        Assert.AreEqual(GameObject.Find("WashingMachine/Score").GetComponent<Text>().text, "1");

        EndFunction();
    }

    [Test]
    public void RefreshAllTest()
    {
        StartFunction();

        Assert.AreEqual(GameObject.Find("Scale/Score").GetComponent<Text>().text, "1");
        Assert.AreEqual(GameObject.Find("TemporalScale/Score").GetComponent<Text>().text, "Week");
        Assert.AreEqual(GameObject.Find("Bath/Score").GetComponent<Text>().text, "0");
        Assert.AreEqual(GameObject.Find("Bathroom/Score").GetComponent<Text>().text, "0");
        Assert.AreEqual(GameObject.Find("DishWasher/Score").GetComponent<Text>().text, "0");
        Assert.AreEqual(GameObject.Find("HandDish/Score").GetComponent<Text>().text, "0");
        Assert.AreEqual(GameObject.Find("Shower/Score").GetComponent<Text>().text, "0");
        Assert.AreEqual(GameObject.Find("WashingMachine/Score").GetComponent<Text>().text, "0");


        sceneData.IncrScale();
        sceneData.SetCurrentTime(SceneData.TimeName.Month);
        sceneData.IncrDataCpt(SceneData.DataName.Bath);
        sceneData.IncrDataCpt(SceneData.DataName.Bathroom);
        sceneData.IncrDataCpt(SceneData.DataName.DishWasher);
        sceneData.IncrDataCpt(SceneData.DataName.HandDish);
        sceneData.IncrDataCpt(SceneData.DataName.Shower);
        sceneData.IncrDataCpt(SceneData.DataName.WashingMachine);

        settingsFunctions.RefreshAll();

        Assert.AreEqual(GameObject.Find("Scale/Score").GetComponent<Text>().text, "2");
        Assert.AreEqual(GameObject.Find("TemporalScale/Score").GetComponent<Text>().text, "Month");
        Assert.AreEqual(GameObject.Find("Bath/Score").GetComponent<Text>().text, "1");
        Assert.AreEqual(GameObject.Find("Bathroom/Score").GetComponent<Text>().text, "1");
        Assert.AreEqual(GameObject.Find("DishWasher/Score").GetComponent<Text>().text, "1");
        Assert.AreEqual(GameObject.Find("HandDish/Score").GetComponent<Text>().text, "1");
        Assert.AreEqual(GameObject.Find("Shower/Score").GetComponent<Text>().text, "1");        
        Assert.AreEqual(GameObject.Find("WashingMachine/Score").GetComponent<Text>().text, "1");

        EndFunction();
    }
}
