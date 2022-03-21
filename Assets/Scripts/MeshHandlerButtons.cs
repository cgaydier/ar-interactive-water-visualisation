using UnityEngine;
using UnityEngine.UI;

/* summary :
 * Linked to MeshHandler
 * Handles the create/validate/clear parts of the mesh
 * 
 * variables :
 * - public - 
 * settings - Settings GameObject
 * textInput - InputField
 * 
 * - private - 
 * errorHandler - Link to ErrorHandler's script
 * placePoints - Link to MeshHandler's script
 * createMesh - Link to MeshHandler's script
 * sceneData - Link to SceneData's script
 * createLine - Link to LineHandler's script
 */
public class MeshHandlerButtons : MonoBehaviour
{
    public GameObject settings;
    public InputField textInput;
    private ErrorHandler errorHandler;
    private PlacePoints placePoints;
    private CreateMesh createMesh;
    private SceneData sceneData;
    private CreateLine createLine;

    public void Start()
    {
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        placePoints = GameObject.Find("MeshHandler").GetComponent<PlacePoints>();
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        createLine = GameObject.Find("LineHandler").GetComponent<CreateLine>();
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
            errorHandler.ErrorMessageReset();
            sceneData.GetEnumState().SetPlacePoints();
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
            errorHandler.ErrorMessageReset();
            sceneData.GetEnumState().SetMainScene();
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
        if (sceneData.IsMeshCreated() || sceneData.GetEnumState().currentState != EnumState.State.MainView)
        {
            errorHandler.ErrorMessageReset();
            sceneData.SetPointsPlaced(false);
            sceneData.SetMeshCreated(false);
            sceneData.GetEnumState().SetMainScene();
            settings.SetActive(false);
            placePoints.ClearAll();
            createMesh.ClearAll();
            ClearSettings();
            Destroy(GameObject.Find("Mesh"));
            textInput.text = "";
            if (GameObject.Find("AverageConso"))
                GameObject.Find("AverageConso").SetActive(false);
            if (sceneData.IsLinesShowned())
                createLine.ClearAll();
            if (GameObject.Find("Legend"))
                GameObject.Find("Legend").SetActive(false);
            sceneData.ClearAll();
        }
        else
        {
            errorHandler.AlreadyClearedError();
        }
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
    
    /* summary : 
     * Clears all the settings
     */
    public void ClearSettings()
    {
        settings.GetComponent<SettingsFunctions>().RefreshAll();
    }
}
