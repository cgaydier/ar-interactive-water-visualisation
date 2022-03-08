using UnityEngine;

public class EnumState : MonoBehaviour
{
    public GameObject Settings;
    public enum State 
    {
        MainView,
        PlacePoints,
        ConsumptionScene,
        Tuto
    }

    public State currentState;

    private void Start()
    {
        SetTuto();
    }

    public State GetState()
    {
        return currentState;
    }

    public void ChangeSettingScene()
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

    public void SetMainScene()
    {
        currentState = State.MainView;
    }

    public void SetPlacePoints()
    {
        currentState = State.PlacePoints;
    }

    public void SetConsumptionScene()
    {
        currentState = State.ConsumptionScene;
    }

    public void SetTuto()
    {
        currentState = State.Tuto;
    }
}
