using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{   
    Mesh mesh;
    GameObject go;
    List<int> triangles = new List<int>();
    public Material meshMat;
    public PlacePoints placePoints;
    float surfaceMesh = 0f;
    float volumeMesh = 0f;
    public CreateLine createLine;
    bool meshCreated = false;
    bool pointsPlaced = false;
    private float offset = 0.001f;

    void Start()
    {
        mesh = new Mesh();
    }

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

    public void AddWater()
    {
        SetWater(offset + 0.01f);
    }
    public void AddWater(float volume)
    {
        SetWater(offset + (volume/surfaceMesh));
    }

    public void RemoveWater()
    {
        SetWater(offset - 0.01f >= 0.00f ? offset - 0.01f : 0.00f);
    }

    public void RemoveWater(float volume)
    {
        SetWater(offset - volume >= 0.00f ? offset - volume : 0.00f);
    }

    public void SetWater(float tmp)
    {
        offset = tmp;
        RefreshMesh();
    }

    void RefreshMesh()
    {
        surfaceMesh = 0f;
        volumeMesh = 0f;
        triangles.Clear();
        mesh.Clear();
        meshCreated = false;
        Destroy(go);
        createLine.ClearAll();
    }

    public void AddLine()
    {
        createLine.AddLine(offset / 2, placePoints.vertices);
    }

    public void ClearAll()
    {
        RefreshMesh();
        pointsPlaced = false;
        GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume : 0 m3";
    }

    float CreateTriangles(List<Vector3> vertices)
    {
        // nb_faces global : (placePoints.nb_vertices/2) + 2
        int nbTotal = vertices.Count;
        //Horizontal
        for (int i = 0, j = 0; i < (nbTotal - 4) / 2; i++, j += 2)
        {
            // Bottom
            triangles.Add(0);
            triangles.Add(j + 4);
            triangles.Add(j + 2);
            surfaceMesh = surfaceMesh + ((Vector3.Distance(vertices[0], vertices[j+2]) * Vector3.Distance(vertices[j+2], vertices[j+4])) / 2f);

            // Top
            triangles.Add(1);
            triangles.Add(j + 5);
            triangles.Add(j + 3);
        }

        // Vertical
        for (int i = 0, j = 0; i < nbTotal / 2; i++, j += 2)
        {
            triangles.Add(j % nbTotal);
            triangles.Add((j + 1) % nbTotal);
            triangles.Add((j + 2) % nbTotal);
            triangles.Add((j + 2) % nbTotal);
            triangles.Add((j + 1) % nbTotal);
            triangles.Add((j + 3) % nbTotal);
        }
        return surfaceMesh;
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
            List<Vector3> tmp = new List<Vector3>();
            for (int i = 0; i < placePoints.vertices.Count; i ++)
            {
                if (i%2 == 0)
                {
                    tmp.Add(new Vector3(placePoints.vertices[i].x,
                                        placePoints.vertices[i].y,
                                        placePoints.vertices[i].z));
                }
                else
                {
                    tmp.Add(new Vector3(placePoints.vertices[i].x,
                                        placePoints.vertices[i].y + offset,
                                        placePoints.vertices[i].z));
                }
            }

            mesh.vertices = tmp.ToArray();
            volumeMesh = CreateTriangles(tmp) * offset; // volume (m3)
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume : " + volumeMesh.ToString("F2") + " m3";
            mesh.triangles = triangles.ToArray();
            mesh.MarkDynamic();
            mesh.Optimize();
            mesh.OptimizeIndexBuffers();
            mesh.OptimizeReorderVertexBuffer();

            go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            go.GetComponent<MeshRenderer>().material = meshMat;
            go.GetComponent<MeshFilter>().mesh = mesh;

            meshCreated = true;

            AddLine();
        }

        else if(Checkpoints() && meshCreated)
        {
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }
    }
}
