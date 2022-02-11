using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumState : MonoBehaviour
{
    public enum State {MainView, DisplayCube, SetScene}

    public State currentState;

    private void Start()
    {
        setMainScene();
    }

    public State GetState()
    {
        return currentState;
    }

    public void setDisplayCube()
    {
        currentState = State.DisplayCube;
    }

    public void changeParamScene()
    {
        if (currentState == State.SetScene)
        {
            currentState = State.MainView;
        }
        else
        {
            currentState = State.SetScene;
        }
    }

    public void setMainScene()
    {
        currentState = State.MainView;
    }
}
