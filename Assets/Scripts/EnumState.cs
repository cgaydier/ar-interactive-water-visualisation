using UnityEngine;

/* summary :
 * Linked to SceneState
 * Handles the current state of the scene for the inputs
 * 
 * variables :
 * - public -
 * State - enum for the differents states
 */
public class EnumState : MonoBehaviour
{
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

    /* summary : 
     * Change currentState when the settings panel is opened or closed
     */
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
