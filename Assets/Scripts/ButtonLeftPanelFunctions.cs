using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeftPanelFunctions : MonoBehaviour
{
    public PlacePoints placePoints;
    public CreateMesh createMesh;

    // Start is called before the first frame update
    public void CreatePointsMesh()
    {
        placePoints.enumState.SetPlacePoints();
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
