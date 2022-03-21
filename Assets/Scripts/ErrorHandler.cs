using UnityEngine;

/* summary : 
 * Linked to ErrorHandler
 * Handles the errors messages displayed to the user
 * 
 * variables :
 * - public -
 * userDebugPanel - UserDebugPanel GameObject
 * 
 * - private - 
 * debugMessage - String to save the message to display
 */
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

    public string GetDebugMessage()
    {
        return debugMessage;
    }
    void Update()
    {
        userDebugPanel.GetComponent<UnityEngine.UI.Text>().text = debugMessage;
    }
}
