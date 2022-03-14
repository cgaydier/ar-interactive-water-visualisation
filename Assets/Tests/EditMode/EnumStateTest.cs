using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnumState
{
    public enum State {MainView, ParamScene, PlacePoints, PlacePointsBefore, ConsumptionScene}
      public State currentState;

    [Test]
    public void SetParamScene_ShouldReturn()
    {
        //SetParamScene();
        Assert.AreEqual(currentState, State.ParamScene);
    }

    [Test]
    public void SetMainScene_ShouldReturn()
    { 
        //SetMainScene();
        Assert.AreEqual(currentState, State.MainView);

    }

    [Test]
    public void SetPlacePoints_ShouldReturn()
    { 
        //SetPlacePoints();
        Assert.AreEqual(currentState, State.PlacePoints);

    }

    [Test]
    public void SetPlacePointsBefore_ShouldReturn()
    { 
        //SetPlacePointsBefore();
        Assert.AreEqual(currentState, State.PlacePointsBefore);

    }

    [Test]
    public void SetConsumptionScene_ShouldReturn()
    { 
        //SetConsumptionScene();
        Assert.AreEqual(currentState, State.PlacePointsBefore);

    }

    public void ChangeParamSceneToParamScene()
    {
        //SetParamScene();
        //ChangeParamScene();
        Assert.AreEqual(currentState, State.ParamScene);
    }

    public void ChangeParamSceneToPlacePoints()
    {
        //SetMainScene();
        //ChangeMainScene();
        Assert.AreEqual(currentState, State.PlacePoints);
    }

    public void ChangeParamSceneToPlacePointsBefore()
    {
        //SetPlacePointsBefore();
        //ChangeMainScene();
        Assert.AreEqual(currentState, State.PlacePointsBefore);
    }

    public void ChangeParamSceneToMainScene()
    {
        //SetMainScene();
        //ChangeMainScene();
        Assert.AreEqual(currentState, State.MainView);
    }

    public void ChangeModalSceneToMainScene()
    {
        //SetConsumptionScene();
        //ChangeModalScene();
        Assert.AreEqual(currentState == State.ConsumptionScene, "Modal ConsumptionScene is not load correctly");
    }

}
