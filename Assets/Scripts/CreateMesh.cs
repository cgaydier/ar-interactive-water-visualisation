using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{   
    Mesh mesh;
    GameObject go;
    SceneDatas sceneDatas;
    List<int> triangles = new List<int>();
    public Material meshMat;
    public PlacePoints placePoints;
    float volumeMesh = 0f;
    public CreateLine createLine;
    float offset = 0.001f;
    float currentOffset = 0.001f;

    void Start()
    {
        mesh = new Mesh();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        //sceneDatas.vertices.Add(new Vector3(0, 0, 0));
        //sceneDatas.vertices.Add(new Vector3(0, 0, 0));
        //sceneDatas.vertices.Add(new Vector3(1, 0, 0));
        //sceneDatas.vertices.Add(new Vector3(1, 0, 0));
        //sceneDatas.vertices.Add(new Vector3(1, 0, 1));
        //sceneDatas.vertices.Add(new Vector3(1, 0, 1));
        //sceneDatas.vertices.Add(new Vector3(0, 0, 1));
        //sceneDatas.vertices.Add(new Vector3(0, 0, 1));
        //sceneDatas.pointsPlaced = true;
    }

    void Update()
    {
        MeshHandler();
    }

    public void AddScale()
    {
        sceneDatas.IncrScale();
        SetWater(offset / sceneDatas.GetScale());
    }

    public void RemoveScale()
    {
        sceneDatas.DecrScale();
        SetWater(offset / sceneDatas.GetScale());
    }

    public void AddWater(float volume)
    {
        offset += volume / sceneDatas.surfaceMesh;
        SetWater(offset / sceneDatas.GetScale());
    }

    public void RemoveWater(float volume)
    {
        offset = offset - (volume / sceneDatas.surfaceMesh) >= 0.00f ? offset - (volume / sceneDatas.surfaceMesh) : 0.00f;
        SetWater(offset / sceneDatas.GetScale());
    }

    public void SetWater(float tmp)
    {
        currentOffset = tmp;
        RefreshMesh();
    }

    void RefreshMesh()
    {
        sceneDatas.surfaceMesh = 0f;
        volumeMesh = 0f;
        triangles.Clear();
        mesh.Clear();
        sceneDatas.meshCreated = false;
        Destroy(go);
        createLine.ClearAll();
    }

    public void ClearAll()
    {
        RefreshMesh();
        offset = 0.001f;
        currentOffset = 0.001f;
        sceneDatas.pointsPlaced = false;
        GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume :\n0 m3";
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
            sceneDatas.surfaceMesh += ((Vector3.Distance(vertices[0], vertices[j+2]) * Vector3.Distance(vertices[j+2], vertices[j+4])) / 2f);

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
        return sceneDatas.surfaceMesh;
    }

    bool Checkpoints()
    {
        int nb_total = (sceneDatas.vertices.Count)/2;
        if (nb_total < 3 || nb_total > 10)
            return false;
        return true;
    }

    void MeshHandler()
    {
        if (Checkpoints() && !sceneDatas.meshCreated && sceneDatas.pointsPlaced)
        {
            List<Vector3> tmp = new List<Vector3>();
            for (int i = 0; i < sceneDatas.vertices.Count; i ++)
            {
                if (i%2 == 0)
                {
                    tmp.Add(new Vector3(sceneDatas.vertices[i].x,
                                        sceneDatas.vertices[i].y,
                                        sceneDatas.vertices[i].z));
                }
                else
                {
                    tmp.Add(new Vector3(sceneDatas.vertices[i].x,
                                        sceneDatas.vertices[i].y + currentOffset,
                                        sceneDatas.vertices[i].z));
                }
            }

            mesh.vertices = tmp.ToArray();
            volumeMesh = CreateTriangles(tmp) * currentOffset; // volume (m3)
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume :\n" + volumeMesh.ToString("F2") + " m3";
            mesh.triangles = triangles.ToArray();
            mesh.MarkDynamic();
            mesh.Optimize();
            mesh.OptimizeIndexBuffers();
            mesh.OptimizeReorderVertexBuffer();

            go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            go.GetComponent<MeshRenderer>().material = meshMat;
            go.GetComponent<MeshFilter>().mesh = mesh;

            sceneDatas.meshCreated = true;
            Debug.Log(currentOffset);
        }

        else if(Checkpoints() && sceneDatas.meshCreated)
        {
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }
    }
}
