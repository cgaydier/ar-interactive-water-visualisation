using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumState : MonoBehaviour
{
    public enum State {MainView, DisplayCube, ParamScene, PlacePoints}

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
        if (currentState != State.PlacePoints)
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
