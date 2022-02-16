using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CreateMesh : MonoBehaviour
{   
    Mesh mesh;
    GameObject go;
    bool mesh_created = false;
    List<int> triangles = new List<int>();
    Material mesh_mat;
    public PlacePoints placePoints;
    bool pointsPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GameObject go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
    }

    // Update is called once per frame
    void Update()
    {
        MeshHandler();
    }

    public void SetPointsPlaced(bool tmp)
    {
        pointsPlaced = tmp;
    }

    void CreateTriangles()
    {
        for(int i = 2; i < placePoints.nb_vertices; i++)
        {
            triangles.Add(0);
            triangles.Add(i-1);
            triangles.Add(i);
        }
    }

    bool Checkpoints()
    {
        if(placePoints.nb_vertices < 3 || placePoints.nb_vertices > 10)
            return false;
        return true;
    }

    void MeshHandler()
    {
        if(Checkpoints() && !mesh_created && pointsPlaced)
        {
            mesh.vertices = placePoints.vertices.ToArray();
            CreateTriangles();
            mesh.triangles = triangles.ToArray();
            go.GetComponent<MeshRenderer>().material = mesh_mat;
            go.GetComponent<MeshFilter>().mesh = mesh;
            go.transform.position = new Vector3(go.transform.position.x, mesh.vertices[0].y, go.transform.position.z);
            for(int i = 0; i < placePoints.nb_vertices; i++)
            {
                Destroy(placePoints.points[i]);
            }
            mesh_created = true;
        }

        else if(Checkpoints() && mesh_created)
        {
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }

        else if(pointsPlaced)
        {
            print("Not enough points (3 min) or too much (10 max).\n Current : " + placePoints.nb_vertices + "\n");
        }
    }
}
