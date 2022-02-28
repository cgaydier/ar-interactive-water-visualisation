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
    private float offset = 0.2f;

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

    public void AddWater()
    {
        SetWater(offset + 0.01f);
    }
    public void AddWater(float volume)
    {
        SetWater(offset + volume);
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
        surface = 0f;
        triangles.Clear();
        mesh.Clear();
        meshCreated = false;
    }
    public void ClearAll()
    {
        pointsPlaced = false;
        meshCreated = false;
        surface = 0f;
        GameObject.Find("SurfaceM2").GetComponent<UnityEngine.UI.Text>().text = "Volume : 0 m3";
        triangles.Clear();
        mesh.Clear();
    }

    float CreateTriangles(List<Vector3> vertices)
    {
        // nb_faces global : (placePoints.nb_vertices/2) + 2
        int nb_total = vertices.Count;
        //Horizontal
        for (int i = 0, j = 0; i < (nb_total - 4) / 2; i++, j += 2)
        {
            // Bottom
            triangles.Add(0);
            triangles.Add(j + 4);
            triangles.Add(j + 2);
            surface = surface + ((Vector3.Distance(vertices[0], vertices[j+2]) * Vector3.Distance(vertices[j+2], vertices[j+4])) / 2f);

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
        return surface * offset;
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
            surface = CreateTriangles(tmp); // volume (m3)
            GameObject.Find("SurfaceM2").GetComponent<UnityEngine.UI.Text>().text = "Volume : " + surface.ToString("F2") + " m3";
            mesh.triangles = triangles.ToArray();
            mesh.MarkDynamic();
            mesh.Optimize();
            mesh.OptimizeIndexBuffers();
            mesh.OptimizeReorderVertexBuffer();

            go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            go.GetComponent<MeshRenderer>().material = mesh_mat;
            go.GetComponent<MeshFilter>().mesh = mesh;

            meshCreated = true;
        }

        else if(Checkpoints() && meshCreated)
        {
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }

        // else if (pointsPlaced)
        // {
        //     print("Not enough points (3 min) or too much (10 max).\n Current : " + (placePoints.vertices.Count / 2)+ "\n");
        // }
    }
}
