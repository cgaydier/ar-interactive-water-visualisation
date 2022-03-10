using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
    public GameObject userDebugPanel;
    private string debugMessage;

    // Start is called before the first frame update
    private void Start()
    {
        debugMessage = "";
        userDebugPanel.SetActive(true);
    }

    public void ErrorMessageReset()
    {
        debugMessage = "";
    }

    public void PlacePointsError()
    {
        debugMessage = "Max points reached (10).\nPlease validate or clear.\n";
    }

    public void NoSurfaceCreatedError()
    {
        debugMessage = "Please place points.\n";
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

    public void NoVolumeError()
    {
        debugMessage = "Please create a water volume.\n";
    }

    // Update is called once per frame
    void Update()
    {
        userDebugPanel.GetComponent<UnityEngine.UI.Text>().text = debugMessage;
    }
}
