using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMesh : MonoBehaviour
{
    public PlacePoints placePoints;
    void ClearM()
    {
        Destroy(GameObject.Find("Mesh"));
        for(int i = 0; i < placePoints.nb_vertices; i++)
            {
                Destroy(placePoints.points[i]);
            }
    }
}
