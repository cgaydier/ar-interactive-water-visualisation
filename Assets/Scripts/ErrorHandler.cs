using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
    public GameObject userDebugPanel;
    private string debugMessage;

    private void Start()
    {
        debugMessage = "";
        userDebugPanel.SetActive(true);
    }

    public void PlacePointsError()
    {
        debugMessage = "Max points reached (10).\nPlease validate or clear.\n";
    }

    public void AlreadyCreatedError()
    {
        debugMessage = "Points already placed.\nPlease validate or clear.\n";
    }
    public void AlreadyValidatedError()
    {
        debugMessage = "Surface already validated.\n";
    }

    public void AlreadyClearedError()
    {
        debugMessage = "Nothing to clear.\n";
    }

    public void NoPointsError()
    {
        debugMessage = "Not enough points.\nAt least 3 are needed.\n";
    }

    public void NoVolumeError()
    {
        debugMessage = "Please create a water volume.\n";
    }

    public void ErrorMessageReset()
    {
        debugMessage = "";
    }

    void Update()
    {
        userDebugPanel.GetComponent<UnityEngine.UI.Text>().text = debugMessage;
    }
}
