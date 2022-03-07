using UnityEngine;
using UnityEngine.UI;

public class MeshHandlerButtons : MonoBehaviour
{
    public GameObject Shower;
    public GameObject Bath;
    public GameObject HandDish;
    public GameObject DishWasher;
    public GameObject WashingMachine;
    public GameObject Bathroom;
    public GameObject Scale;
    public InputField textInput;

    private PlacePoints placePoints;
    private CreateMesh createMesh;
    private SceneDatas sceneDatas;

    public void Start()
    {
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        placePoints = GameObject.Find("MeshHandler").GetComponent<PlacePoints>();
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
    }

    public void CreatePointsMesh()
    {
        if (!sceneDatas.IsMeshCreated())
        {
            sceneDatas.enumState.SetPlacePointsBefore();
            sceneDatas.SetPointsPlaced(false);
        } 
    }

    public void ValidatePointsMesh()
    {
        sceneDatas.enumState.SetParamScene();
        placePoints.ClearAll();
        sceneDatas.SetPointsPlaced(true);
    }

    public void ClearPointsMesh()
    {
        sceneDatas.enumState.SetParamScene();
        placePoints.ClearAll();
        sceneDatas.ClearAll();
        createMesh.ClearAll();
        ClearSettings();
        Destroy(GameObject.Find("Mesh"));
        textInput.text = "";
    }

    public void CreateArbitraryMesh()
    {
        if (sceneDatas.IsMeshCreated())
        {
            ClearSettings();
            string textInField = textInput.text;
            int tmp;
            int.TryParse(textInField, out tmp);
            createMesh.SetCustomVolume((float)tmp);
        }
    }

    public void ClearSettings()
    {
        Shower.GetComponent<SettingsFunctions>().RefreshText();
        Bath.GetComponent<SettingsFunctions>().RefreshText();
        HandDish.GetComponent<SettingsFunctions>().RefreshText();
        DishWasher.GetComponent<SettingsFunctions>().RefreshText();
        WashingMachine.GetComponent<SettingsFunctions>().RefreshText();
        Bathroom.GetComponent<SettingsFunctions>().RefreshText();
        Scale.GetComponent<SettingsFunctions>().RefreshText();
    }
}
