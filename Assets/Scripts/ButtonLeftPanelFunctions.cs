using UnityEngine;

public class ButtonLeftPanelFunctions : MonoBehaviour
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
        placePoints.clearAll();
        createMesh.ClearAll();
        Destroy(GameObject.Find("Mesh"));
    }
}
