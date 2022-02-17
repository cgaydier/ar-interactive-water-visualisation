using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CreateMesh : MonoBehaviour
{   
    Mesh mesh;
    GameObject go;
    List<int> triangles = new List<int>();
    public Material mesh_mat;
    public PlacePoints placePoints;
    bool meshCreated = false;
    bool pointsPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
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

    public void SetMeshCreated(bool tmp)
    {
        meshCreated = tmp;
    }
    public void ClearAll()
    {
        pointsPlaced = false;
        meshCreated = false;
        triangles.Clear();
        mesh.Clear();
    }

    void CreateTriangles()
    {
        // nb_faces global : (placePoints.nb_vertices/2) + 2
        int nb_total = placePoints.vertices.Count;
        //Horizontal
        for (int i = 0, j = 0; i < (nb_total - 4) / 2; i++, j += 2)
        {
            // Bottom
            triangles.Add(0);
            triangles.Add(j + 2);
            triangles.Add(j + 4);

            // Top
            triangles.Add(1);
            triangles.Add(j + 5);
            triangles.Add(j + 3);
        }

        // Vertical
        for (int i = 1, j = 0; i < nb_total / 2; i++, j += 2)
        {
            triangles.Add(j % nb_total);
            triangles.Add((j + 1) % nb_total);
            triangles.Add((j + 2) % nb_total);
            triangles.Add((j + 2) % nb_total);
            triangles.Add((j + 1) % nb_total);
            triangles.Add((j + 3) % nb_total);
        }

        //for (int i = 2; i < placePoints.nb_vertices; i++)
        //{
        //    triangles.Add(0);
        //    triangles.Add(i - 1);
        //    triangles.Add(i);
        //}
    }

    bool Checkpoints()
    {
        int nb_total = placePoints.vertices.Count;
        if (nb_total < 3 || nb_total > 10)
            return false;
        return true;
    }

    void MeshHandler()
    {
        if(Checkpoints() && !meshCreated && pointsPlaced)
        {
            mesh.vertices = placePoints.vertices.ToArray();
            CreateTriangles();
            mesh.triangles = triangles.ToArray();
            mesh.MarkDynamic();
            mesh.Optimize();
            mesh.OptimizeIndexBuffers();
            mesh.OptimizeReorderVertexBuffer();

            go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            go.GetComponent<MeshRenderer>().material = mesh_mat;
            go.GetComponent<MeshFilter>().mesh = mesh;
            //go.transform.position = new Vector3(go.transform.position.x, mesh.vertices[0].y, go.transform.position.z);

            placePoints.clearAll();

            meshCreated = true;
        }

        else if(Checkpoints() && meshCreated)
        {
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }

        else if (pointsPlaced)
        {
            print("Not enough points (3 min) or too much (10 max).\n Current : " + placePoints.vertices.Count + "\n");
        }
    }
}
