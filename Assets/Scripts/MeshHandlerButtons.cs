using UnityEngine;

public class MeshHandlerButtons : MonoBehaviour
{
    public GameObject Shower;
    public GameObject Bath;
    public GameObject HandDish;
    public GameObject DishWasher;
    public GameObject WashingMachine;
    public GameObject Bathroom;
    public GameObject Scale;

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
    }

    public void CreateArbitraryMesh(string textInField)
    {
        ClearSettings();
        createMesh.SetCustomVolume(int.Parse(textInField));
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
