using UnityEngine;
using UnityEngine.UI;

public class MeshHandlerButtons : MonoBehaviour
{
    public GameObject Settings;
    public InputField textInput;
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
    }

    public void CreatePointsMesh()
    {
        if (sceneDatas.IsMeshCreated())
            errorHandler.AlreadyCreatedError();

        else
        {
            errorHandler.ErrorMessageReset();
            sceneDatas.enumState.SetPlacePoints();
            sceneDatas.SetPointsPlaced(false);
        }
    }

    public void ValidatePointsMesh()
    {
        if(sceneDatas.IsPointsPlaced())
            errorHandler.AlreadyValidatedError();
        
        else
        {
            errorHandler.ErrorMessageReset();
            sceneDatas.enumState.SetMainScene();
            placePoints.ClearAll();
            sceneDatas.SetPointsPlaced(true);
        }
    }

    public void ClearPointsMesh()
    {
        if(!sceneDatas.IsMeshCreated())
            errorHandler.AlreadyClearedError();

        else
        {
            errorHandler.ErrorMessageReset();
            sceneDatas.enumState.SetMainScene();
            placePoints.ClearAll();
            sceneDatas.ClearAll();
            createMesh.ClearAll();
            ClearSettings();
            Destroy(GameObject.Find("Mesh"));
            textInput.text = "";
        }
    }

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

    public void ResetMeshSettings()
    { 
        sceneDatas.ClearCpt();
        createMesh.Reset();
        ClearSettings();
        textInput.text = "";
    }
    
    public void ClearSettings()
    {
        Settings.GetComponent<SettingsFunctions>().RefreshAll();
    }
}
