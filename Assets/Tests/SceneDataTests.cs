using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SceneDataTests
{
    private SceneData sceneData;

    private void StartFunction()
    {
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        sceneData.Start();
    }

    [Test]
    public void ClearCptTest()
    {
        StartFunction();

        sceneData.IncrDataCpt(SceneData.DataName.Bath);
        sceneData.IncrDataCpt(SceneData.DataName.Bathroom);
        sceneData.IncrDataCpt(SceneData.DataName.Bathroom);
        sceneData.IncrDataCpt(SceneData.DataName.DishWasher);
        sceneData.IncrDataCpt(SceneData.DataName.HandDish);
        sceneData.IncrDataCpt(SceneData.DataName.HandDish);
        sceneData.IncrDataCpt(SceneData.DataName.HandDish);
        sceneData.IncrDataCpt(SceneData.DataName.Shower);
        sceneData.IncrDataCpt(SceneData.DataName.WashingMachine);
        sceneData.IncrDataCpt(SceneData.DataName.WashingMachine);
        sceneData.IncrDataCpt(SceneData.DataName.WashingMachine);
        sceneData.IncrDataCpt(SceneData.DataName.WashingMachine);

        sceneData.IncrScale();
        sceneData.IncrScale();
        sceneData.IncrScale();
        sceneData.IncrScale();

        sceneData.SetCurrentTime(SceneData.TimeName.Day);

        sceneData.ClearCpt();

        foreach (int i in System.Enum.GetValues(typeof(SceneData.DataName)))
        {
            SceneData.DataName dataName = (SceneData.DataName)i;
            Assert.AreEqual(sceneData.GetDataCpt(dataName), 0);
        }

        Assert.AreEqual(sceneData.GetScale(), 1);
        Assert.AreEqual(sceneData.GetCurrentTime(), SceneData.TimeName.Week);
    }

    [Test]
    public void ClearAllTest()
    {
        StartFunction();

        sceneData.SetMeshCreated(true);
        sceneData.SetPointsPlaced(true);
        sceneData.SetLinesShowned(true);
        sceneData.SetSurfaceMesh(2.0f);

        sceneData.ClearAll();

        Assert.AreEqual(sceneData.IsMeshCreated(), false);
        Assert.AreEqual(sceneData.IsPointsPlaced(), false);
        Assert.AreEqual(sceneData.IsLinesShowned(), false);
        Assert.AreEqual(sceneData.GetSurfaceMesh(), 0f);
    }

    [Test]
    public void ClearVerticesTest()
    {
        StartFunction();

        sceneData.AddVertice(new Vector3(1, 1, 1));
        sceneData.AddVertice(new Vector3(1, 1, 1));
        sceneData.AddVertice(new Vector3(1, 1, 1));

        sceneData.ClearVertices();

        Assert.AreEqual(sceneData.GetVertices().Count, 0);
    }

    [Test]
    public void GetEnumStateTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetEnumState(), GameObject.Find("SceneState").GetComponent<EnumState>());
    }

    [Test]
    public void GetSetCurrentTimeTest()
    {
        StartFunction();

        sceneData.SetCurrentTime(SceneData.TimeName.Day);

        Assert.AreEqual(sceneData.GetCurrentTime(), SceneData.TimeName.Day);
    }


    [Test]
    public void GetDataConsumptionTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetDataConsumption(SceneData.DataName.Bath), 0.15f);
        Assert.AreEqual(sceneData.GetDataConsumption(SceneData.DataName.Bathroom), 0.009f);
        Assert.AreEqual(sceneData.GetDataConsumption(SceneData.DataName.DishWasher), 0.014f);
        Assert.AreEqual(sceneData.GetDataConsumption(SceneData.DataName.HandDish), 0.017f);
        Assert.AreEqual(sceneData.GetDataConsumption(SceneData.DataName.Shower), 0.06f);
        Assert.AreEqual(sceneData.GetDataConsumption(SceneData.DataName.WashingMachine), 0.07f);
    }

    [Test]
    public void GetExampleConsumptionTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetExampleConsumption(SceneData.ExampleName.AutoWash), 0.2f);
        Assert.AreEqual(sceneData.GetExampleConsumption(SceneData.ExampleName.Average1P1W), 1.03f);
        Assert.AreEqual(sceneData.GetExampleConsumption(SceneData.ExampleName.DripTap1W), 0.67f);
        Assert.AreEqual(sceneData.GetExampleConsumption(SceneData.ExampleName.Garden1W), 0.38f);
        Assert.AreEqual(sceneData.GetExampleConsumption(SceneData.ExampleName.ToiletFlushLeak1W), 4.2f);
    }

    [Test]
    public void GetDataColorTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetDataColor(SceneData.DataName.Bath), Color.red);
        Assert.AreEqual(sceneData.GetDataColor(SceneData.DataName.Bathroom), Color.yellow);
        Assert.AreEqual(sceneData.GetDataColor(SceneData.DataName.DishWasher), Color.green);
        Assert.AreEqual(sceneData.GetDataColor(SceneData.DataName.HandDish), Color.cyan);
        Assert.AreEqual(sceneData.GetDataColor(SceneData.DataName.Shower), Color.blue);
        Assert.AreEqual(sceneData.GetDataColor(SceneData.DataName.WashingMachine), Color.magenta);
    }

    [Test]
    public void IncrGetDataCptTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bath), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bathroom), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.DishWasher), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.HandDish), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Shower), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.WashingMachine), 0);

        sceneData.IncrDataCpt(SceneData.DataName.Bath);
        sceneData.IncrDataCpt(SceneData.DataName.Bathroom);
        sceneData.IncrDataCpt(SceneData.DataName.DishWasher);
        sceneData.IncrDataCpt(SceneData.DataName.HandDish);
        sceneData.IncrDataCpt(SceneData.DataName.Shower);
        sceneData.IncrDataCpt(SceneData.DataName.WashingMachine);

        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bath), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bathroom), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.DishWasher), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.HandDish), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Shower), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.WashingMachine), 1);
    }

    [Test]
    public void DecrGetDataCptTest()
    {
        StartFunction();

        sceneData.IncrDataCpt(SceneData.DataName.Bath);
        sceneData.IncrDataCpt(SceneData.DataName.Bathroom);
        sceneData.IncrDataCpt(SceneData.DataName.DishWasher);
        sceneData.IncrDataCpt(SceneData.DataName.HandDish);
        sceneData.IncrDataCpt(SceneData.DataName.Shower);
        sceneData.IncrDataCpt(SceneData.DataName.WashingMachine);

        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bath), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bathroom), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.DishWasher), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.HandDish), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Shower), 1);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.WashingMachine), 1);

        sceneData.DecrDataCpt(SceneData.DataName.Bath);
        sceneData.DecrDataCpt(SceneData.DataName.Bathroom);
        sceneData.DecrDataCpt(SceneData.DataName.DishWasher);
        sceneData.DecrDataCpt(SceneData.DataName.HandDish);
        sceneData.DecrDataCpt(SceneData.DataName.Shower);
        sceneData.DecrDataCpt(SceneData.DataName.WashingMachine);

        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bath), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Bathroom), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.DishWasher), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.HandDish), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.Shower), 0);
        Assert.AreEqual(sceneData.GetDataCpt(SceneData.DataName.WashingMachine), 0);
    }


    [Test]
    public void IncrGetScaleTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetScale(), 1);

        sceneData.IncrScale();

        Assert.AreEqual(sceneData.GetScale(), 2);
    }

    [Test]
    public void DecrGetScaleTest()
    {
        StartFunction();

        sceneData.IncrScale();

        Assert.AreEqual(sceneData.GetScale(), 2);

        sceneData.DecrScale();

        Assert.AreEqual(sceneData.GetScale(), 1);
    }

    [Test]
    public void IsSetMeshCreatedTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.IsMeshCreated(), false);

        sceneData.SetMeshCreated(true);

        Assert.AreEqual(sceneData.IsMeshCreated(), true);
    }

    [Test]
    public void IsSetPointPlacedTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.IsPointsPlaced(), false);

        sceneData.SetPointsPlaced(true);

        Assert.AreEqual(sceneData.IsPointsPlaced(), true);
    }

    [Test]
    public void IsSetLinesShownedTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.IsLinesShowned(), false);

        sceneData.SetLinesShowned(true);

        Assert.AreEqual(sceneData.IsLinesShowned(), true);
    }

    [Test]
    public void GetMinPointsTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetMinPoints(), 3);
    }

    [Test]
    public void GetMaxPointsTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetMaxPoints(), 10);
    }

    [Test]
    public void GetDefaultOffsetTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetDefaultOffset(), 0.0001f);
    }

    [Test]
    public void GetSetSurfaceMeshTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetSurfaceMesh(), 0f);

        sceneData.SetSurfaceMesh(1f);

        Assert.AreEqual(sceneData.GetSurfaceMesh(), 1f);
    }

    [Test]
    public void AddGetVerticeTest()
    {
        StartFunction();

        List<Vector3> tmp = sceneData.GetVertices();

        Assert.AreEqual(tmp.Count, 0);

        sceneData.AddVertice(new Vector3(1, 1, 1));
        sceneData.AddVertice(new Vector3(1, 2, 3));
        sceneData.AddVertice(new Vector3(3, 1, 1));

        tmp = sceneData.GetVertices();

        Assert.AreEqual(tmp.Count, 3);
        Assert.AreEqual(tmp[0], new Vector3(1, 1, 1));
        Assert.AreEqual(tmp[1], new Vector3(1, 2, 3));
        Assert.AreEqual(tmp[2], new Vector3(3, 1, 1));
    }

    [Test]
    public void GetVerticesSizeTest()
    {
        StartFunction();

        Assert.AreEqual(sceneData.GetVerticesSize(), 0);

        sceneData.AddVertice(new Vector3(1, 1, 1));
        sceneData.AddVertice(new Vector3(1, 2, 3));
        sceneData.AddVertice(new Vector3(3, 1, 1));

        Assert.AreEqual(sceneData.GetVerticesSize(), 3);

    }
}
