using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{   
    Mesh mesh;
    GameObject go;
    List<int> triangles = new List<int>();
    public Material mesh_mat;
    public PlacePoints placePoints;
    float surface = 0f;
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
        surface = 0f;
        GameObject.Find("SurfaceM2").GetComponent<UnityEngine.UI.Text>().text = "Surface : " + surface.ToString("F2") + "m2";
        triangles.Clear();
        mesh.Clear();
    }

    float CreateTriangles()
    {
        // nb_faces global : (placePoints.nb_vertices/2) + 2
        int nb_total = placePoints.vertices.Count;
        //Horizontal
        for (int i = 0, j = 0; i < (nb_total - 4) / 2; i++, j += 2)
        {
            // Bottom
            triangles.Add(0);
            triangles.Add(j + 4);
            triangles.Add(j + 2);
            print("longueur 0 et " + (j+2) + " : " + (Vector3.Distance(placePoints.vertices[0], placePoints.vertices[j+2])).ToString("F2"));
            print("longueur " + (j+2) + " et " + (j+4) + " : " + (Vector3.Distance(placePoints.vertices[j+2], placePoints.vertices[j+4])).ToString("F2"));
            surface = surface + ((Vector3.Distance(placePoints.vertices[0], placePoints.vertices[j+2]) * Vector3.Distance(placePoints.vertices[j+2], placePoints.vertices[j+4])) / 2);

            // Top
            triangles.Add(1);
            triangles.Add(j + 5);
            triangles.Add(j + 3);
        }

        // Vertical
        for (int i = 0, j = 0; i < nb_total / 2; i++, j += 2)
        {
            triangles.Add(j % nb_total);
            triangles.Add((j + 1) % nb_total);
            triangles.Add((j + 2) % nb_total);
            triangles.Add((j + 2) % nb_total);
            triangles.Add((j + 1) % nb_total);
            triangles.Add((j + 3) % nb_total);
        }
        return surface;
    }

    bool Checkpoints()
    {
        int nb_total = (placePoints.vertices.Count)/2;
        if (nb_total < 3 || nb_total > 10)
            return false;
        return true;
    }

    void MeshHandler()
    {
        if(Checkpoints() && !meshCreated && pointsPlaced)
        {
            mesh.vertices = placePoints.vertices.ToArray();
            // convert to meter
            surface = CreateTriangles();
            GameObject.Find("SurfaceM2").GetComponent<UnityEngine.UI.Text>().text = "Surface : " + surface.ToString("F2") + " m2";
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
            print("Not enough points (3 min) or too much (10 max).\n Current : " + (placePoints.vertices.Count / 2)+ "\n");
        }
    }
}
