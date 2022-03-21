using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnumStateTests
{
    private EnumState enumState;

    private void StartFunction()
    {
        enumState = GameObject.Find("SceneState").GetComponent<EnumState>();
    }

    // A Test behaves as an ordinary method
    [Test]
    public void GetStateTest()
    {
        StartFunction();
        enumState.currentState = EnumState.State.MainView;
        Assert.AreEqual(enumState.GetState(), EnumState.State.MainView);
    }

    [Test]
    public void ChangeSettingSceneTest()
    {
        StartFunction();
        enumState.currentState = EnumState.State.MainView;
        enumState.ChangeSettingScene();

        Assert.AreEqual(enumState.GetState(), EnumState.State.ConsumptionScene);

        enumState.currentState = EnumState.State.PlacePoints;
        enumState.ChangeSettingScene();

        Assert.AreEqual(enumState.GetState(), EnumState.State.ConsumptionScene);

        enumState.ChangeSettingScene();

        Assert.AreEqual(enumState.GetState(), EnumState.State.MainView);
    }

    [Test]
    public void SetMainSceneTest()
    {
        StartFunction();
        enumState.currentState = EnumState.State.PlacePoints;
        enumState.SetMainScene();
        Assert.AreEqual(enumState.GetState(), EnumState.State.MainView);
    }

    [Test]
    public void SetPlacePointsTest()
    {
        StartFunction();
        enumState.currentState = EnumState.State.MainView;
        enumState.SetPlacePoints();
        Assert.AreEqual(enumState.GetState(), EnumState.State.PlacePoints);
    }

    [Test]
    public void SetConsumptionSceneTest()
    {
        StartFunction();
        enumState.currentState = EnumState.State.MainView;
        enumState.SetConsumptionScene();
        Assert.AreEqual(enumState.GetState(), EnumState.State.ConsumptionScene);
    }

    [Test]
    public void SetTutoTest()
    {
        StartFunction();
        enumState.currentState = EnumState.State.MainView;
        enumState.SetTuto();
        Assert.AreEqual(enumState.GetState(), EnumState.State.Tuto);
    }
}
