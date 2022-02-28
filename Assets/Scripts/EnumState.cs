using UnityEngine;

public class EnumState : MonoBehaviour
{
    public GameObject Modal;
    public enum State {MainView, DisplayCube, ParamScene, PlacePoints, PlacePointsBefore, ConsumptionScene}

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
        if (Modal.activeSelf)
        {
            Modal.SetActive(false);
        }
        if (currentState != State.ParamScene && currentState != State.PlacePointsBefore && currentState != State.PlacePoints)
        {
            currentState = State.ParamScene;
        }
        else if (currentState == State.PlacePoints)
        {
            currentState = State.PlacePointsBefore;
        }
        else if (currentState == State.PlacePointsBefore)
        {
            currentState = State.PlacePoints;
        }
        else
        {
            currentState = State.MainView;
        }
    }

    public void ChangeModalScene()
    {
        if (currentState != State.MainView && currentState != State.PlacePoints)
        {
            SetMainScene();
        }
        else
        {
            SetConsumptionScene();
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

    public void SetPlacePointsBefore()
    {
        currentState = State.PlacePointsBefore;
    }

    public void SetConsumptionScene()
    {
        currentState = State.ConsumptionScene;
    }
}
