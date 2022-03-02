using UnityEngine;

public class MeshHandlerButtons : MonoBehaviour
{
    public PlacePoints placePoints;
    public CreateMesh createMesh;

    public void CreatePointsMesh()
    {
        placePoints.enumState.SetPlacePointsBefore();
        createMesh.SetPointsPlaced(false);
    }

    public void ValidatePointsMesh()
    {
        placePoints.enumState.SetParamScene();
        createMesh.SetPointsPlaced(true);
    }

    public void ClearPointsMesh()
    {
        placePoints.enumState.SetParamScene();
        placePoints.ClearAll();
        createMesh.ClearAll();
        Destroy(GameObject.Find("Mesh"));
    }
}
