using UnityEngine;

public class MeshHandlerButtons : MonoBehaviour
{
    public PlacePoints placePoints;
    public CreateMesh createMesh;
    SceneDatas sceneDatas;

    public void Start()
    {
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
    }

    public void CreatePointsMesh()
    {
        placePoints.enumState.SetPlacePointsBefore();
        sceneDatas.pointsPlaced = false;
    }

    public void ValidatePointsMesh()
    {
        placePoints.enumState.SetParamScene();
        placePoints.ClearValidate();
        sceneDatas.pointsPlaced = true;
    }

    public void ClearPointsMesh()
    {
        placePoints.enumState.SetParamScene();
        placePoints.ClearAll();
        createMesh.ClearAll();
        Destroy(GameObject.Find("Mesh"));
    }
}
