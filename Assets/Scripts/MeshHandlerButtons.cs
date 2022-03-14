using UnityEngine;
using UnityEngine.UI;

public class MeshHandlerButtons : MonoBehaviour
{
    public GameObject settings;
    public InputField textInput;
    private Sprite createButtonOn;
    private Sprite createButtonOff;
    private ErrorHandler errorHandler;
    private PlacePoints placePoints;
    private CreateMesh createMesh;
    private SceneDatas sceneDatas;

    public void Start()
    {
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        placePoints = GameObject.Find("MeshHandler").GetComponent<PlacePoints>();
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        createButtonOn = Resources.LoadAll<Sprite>("create_click")[0];
        createButtonOff = Resources.LoadAll<Sprite>("create")[0];
    }

    // Called on touch of the Create button
    // Let the user place points
    public void CreatePointsMesh()
    {
        if (sceneDatas.IsMeshCreated())
            errorHandler.AlreadyCreatedError();

        else
        {
            GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = createButtonOn;
            errorHandler.ErrorMessageReset();
            sceneDatas.enumState.SetPlacePoints();
            sceneDatas.SetPointsPlaced(false);
        }
    }

    // Called on touch of the Validate button
    // Creates a surface depending on the placed points
    public void ValidatePointsMesh()
    {
        if(sceneDatas.IsMeshCreated())
            errorHandler.AlreadyValidatedError();

        else if(placePoints.PointsCount() < sceneDatas.GetMinPoints())
            errorHandler.NoPointsError();

        else
        {
            GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = createButtonOn;
            errorHandler.ErrorMessageReset();
            sceneDatas.enumState.SetMainScene();
            placePoints.ClearAll();
            sceneDatas.SetPointsPlaced(true);
        }
    }

    // Called on touch of the Clear button
    // Clears everything
    public void ClearPointsMesh()
    {
        if(sceneDatas.IsMeshCreated() || sceneDatas.enumState.currentState != EnumState.State.MainView)
        {
            errorHandler.ErrorMessageReset();
            sceneDatas.SetPointsPlaced(false);
            sceneDatas.SetMeshCreated(false);
            sceneDatas.enumState.SetMainScene();
            settings.SetActive(false);
            placePoints.ClearAll();
            sceneDatas.ClearAll();
            createMesh.ClearAll();
            ClearSettings();
            Destroy(GameObject.Find("Mesh"));
            textInput.text = "";
            GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = createButtonOff;
        }
        else
        {
            errorHandler.AlreadyClearedError();
        }
        GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = createButtonOff;
    }

    // Called when an arbitrary value is entered in the settings' section
    // Creates a volume depending on the surface already created and the custom value entered
    public void CreateArbitraryMesh()
    {
        sceneDatas.ClearCpt();
        ClearSettings();
        if (sceneDatas.IsMeshCreated())
        {
            string textInField = textInput.text;
            int tmp;
            int.TryParse(textInField, out tmp);
            createMesh.SetCustomVolume((float)tmp);
        }
    }

    // Called on touch of the Reset button
    // Reset the settings and the mesh to display the starting surface
    public void ResetMeshSettings()
    { 
        sceneDatas.ClearCpt();
        createMesh.Reset();
        ClearSettings();
        textInput.text = "";
    }
    
    public void ClearSettings()
    {
        settings.GetComponent<SettingsFunctions>().RefreshAll();
    }
}
