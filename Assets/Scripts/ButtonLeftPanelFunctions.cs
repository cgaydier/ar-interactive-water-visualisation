using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeftPanelFunctions : MonoBehaviour
{
    public PlacePoints placePoints;
    public CreateMesh createMesh;
    public ClearMesh clearMesh;

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
        createMesh.SetPointsPlaced(false);
        createMesh.SetMeshCreated(false);
        clearMesh.ClearM();
    }
}
