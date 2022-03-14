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
    private SceneData sceneData;

    public void Start()
    {
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        placePoints = GameObject.Find("MeshHandler").GetComponent<PlacePoints>();
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        createButtonOn = Resources.LoadAll<Sprite>("create_click")[0];
        createButtonOff = Resources.LoadAll<Sprite>("create")[0];
    }

    /* summary :
    * Called on touch of the Create button
    * Lets the user place points
    */ 
    public void CreatePointsMesh()
    {
        if (sceneData.IsMeshCreated())
            errorHandler.AlreadyCreatedError();

        else
        {
            GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = createButtonOn;
            errorHandler.ErrorMessageReset();
            sceneData.enumState.SetPlacePoints();
            sceneData.SetPointsPlaced(false);
        }
    }

    /* summary :
    * Called on touch of the Validate button
    * Creates a surface depending on the placed points
    */
    public void ValidatePointsMesh()
    {
        if (sceneData.IsMeshCreated())
            errorHandler.AlreadyValidatedError();

        else if (placePoints.PointsCount() < sceneData.GetMinPoints())
            errorHandler.NoPointsError();

        else
        {
            GameObject.Find("CreateButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite = createButtonOff;
            errorHandler.ErrorMessageReset();
            sceneData.enumState.SetMainScene();
            placePoints.ClearAll();
            sceneData.SetPointsPlaced(true);
        }
    }

    /* summary :
    * Called on touch of the Clear button
    * Clears everything
    */
    public void ClearPointsMesh()
    {
        if (sceneData.IsMeshCreated() || sceneData.enumState.currentState != EnumState.State.MainView)
        {
            errorHandler.ErrorMessageReset();
            sceneData.SetPointsPlaced(false);
            sceneData.SetMeshCreated(false);
            sceneData.enumState.SetMainScene();
            settings.SetActive(false);
            placePoints.ClearAll();
            sceneData.ClearAll();
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

    /* summary :
    * Called when an arbitrary value is entered in the settings' section
    * Creates a volume depending on the surface already created and the custom value entered
    */
    public void CreateArbitraryMesh()
    {
        sceneData.ClearCpt();
        ClearSettings();
        if (sceneData.IsMeshCreated())
        {
            string textInField = textInput.text;
            int tmp;
            int.TryParse(textInField, out tmp);
            createMesh.SetCustomVolume((float)tmp);
        }
    }

    /* summary :
    * Called on touch of the Reset button
    * Resets the settings and the mesh to display the starting surface
    */
    public void ResetMeshSettings()
    { 
        sceneData.ClearCpt();
        createMesh.Reset();
        ClearSettings();
        textInput.text = "";
    }
    
    public void ClearSettings()
    {
        settings.GetComponent<SettingsFunctions>().RefreshAll();
    }
}
