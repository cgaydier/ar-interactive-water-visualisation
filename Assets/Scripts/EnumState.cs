using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumState : MonoBehaviour
{
    public enum State {MainView, DisplayCube, ParamScene, PlacePoints, PlacePointsBefore}

    public State currentState;

    private void Start()
    {
        SetMainScene();
    }

    public State GetState()
    {
        return currentState;
    }

    public void SetDisplayCube()
    {
        currentState = State.DisplayCube;
    }

    public void ChangeParamScene()
    {
        if (currentState != State.PlacePoints && currentState != State.PlacePointsBefore)
        {
            if (currentState == State.MainView)
            {
                currentState = State.ParamScene;
            }
            else
            {
                currentState = State.MainView;
            }
        }
        else if (currentState == State.PlacePoints)
        {
            currentState = State.PlacePointsBefore;
        }
        else
        {
            currentState = State.PlacePoints;
        }
    }

    public void SetParamScene()
    {
        currentState = State.ParamScene;
    }

    public void SetMainScene()
    {
        currentState = State.MainView;
    }

    public void SetPlacePoints()
    {
        currentState = State.PlacePoints;
    }
}
