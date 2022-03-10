using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{   
    private SceneDatas sceneDatas;
    private CreateLine createLine;
    private Mesh mesh;
    private GameObject go;
    private ErrorHandler errorHandler;
    private readonly List<int> triangles = new List<int>();
    private float volumeMesh;
    private float offset;
    private float currentOffset;

    void Start()
    {
        mesh = new Mesh();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        createLine = GameObject.Find("LineHandler").GetComponent<CreateLine>();
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        volumeMesh = 0f;
        offset = sceneDatas.GetDefaultOffset();
        currentOffset = sceneDatas.GetDefaultOffset();
        // sceneDatas.AddVertice(new Vector3(0, 0, 0));
        // sceneDatas.AddVertice(new Vector3(0, 0, 0));
        // sceneDatas.AddVertice(new Vector3(1, 0, 0));
        // sceneDatas.AddVertice(new Vector3(1, 0, 0));
        // sceneDatas.AddVertice(new Vector3(1, 0, 1));
        // sceneDatas.AddVertice(new Vector3(1, 0, 1));
        // sceneDatas.AddVertice(new Vector3(0, 0, 1));
        // sceneDatas.AddVertice(new Vector3(0, 0, 1));
        // sceneDatas.SetPointsPlaced(true);
    }

    void Update()
    {
        MeshHandler();
    }

    public void AddScale()
    {
        sceneDatas.IncrScale();
        SetWater();
    }

    public void DecrScale()
    {
        SetWater();
    }

    public void AddWater(float volume)
    {
        offset += (volume / sceneDatas.GetSurfaceMesh());
        SetWater();
    }

    public void RemoveWater(float volume)
    {
        offset = offset - (volume / sceneDatas.GetSurfaceMesh()) >= 0.00f ? offset - (volume / sceneDatas.GetSurfaceMesh()) : 0.00f;
        SetWater();
    }

    public void SetCustomVolume(float volume){
        if(volume >= 0f)
        {
            offset = (volume / sceneDatas.GetSurfaceMesh());
            SetWater();
        }
    }
    public void SetWater()
    {
        switch (sceneDatas.currentTime)
        {
            case SceneDatas.TimeName.Day:
                currentOffset = offset / 7f;
                break;

            case SceneDatas.TimeName.Week:
                currentOffset = offset;
                break;

            case SceneDatas.TimeName.Month:
                currentOffset = offset * 4;
                break;

            case SceneDatas.TimeName.Year:
                currentOffset = offset * 52;
                break;

            default:
                Debug.Log("Unknown Time type !" + sceneDatas.currentTime);
                break;
        }
        currentOffset /= (float)sceneDatas.GetScale();
        RefreshMesh();
    }

    public void Reset()
    {
        if(sceneDatas.IsMeshCreated())
        {
            currentOffset = sceneDatas.GetDefaultOffset();
            offset = sceneDatas.GetDefaultOffset();
            RefreshMesh();
        }
        else
        {
            errorHandler.NoVolumeError();
        }
    }

    private void RefreshMesh()
    {
        sceneDatas.SetSurfaceMesh(0f);
        volumeMesh = 0f;
        triangles.Clear();
        mesh.Clear();
        sceneDatas.SetMeshCreated(false);
        Destroy(go);
        createLine.ClearAll();
    }

    public void ClearAll()
    {
        RefreshMesh();
        offset = sceneDatas.GetDefaultOffset();
        currentOffset = sceneDatas.GetDefaultOffset();
        sceneDatas.SetPointsPlaced(false);
        if (GameObject.Find("WaterVolumeText"))
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume :\n0 m3";
    }

    private void CreateTriangles(List<Vector3> vertices)
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
            sceneDatas.SetSurfaceMesh(sceneDatas.GetSurfaceMesh() + ((Vector3.Distance(vertices[0], vertices[j+2]) * Vector3.Distance(vertices[j+2], vertices[j+4])) / 2f));

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
    }
    
    private void VolumeMeshCalcul()
    {
        volumeMesh = sceneDatas.GetSurfaceMesh() * currentOffset;
    }

    private bool Checkpoints()
    {
        int nbTotal = (sceneDatas.GetVerticesSize())/2;
        if (nbTotal < sceneDatas.GetMinPoints() || nbTotal > sceneDatas.GetMaxPoints())
            return false;
        return true;
    }

    public void UpdateVolumeText()
    {
        if (sceneDatas.GetScale() > 1)
        {
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Visible volume divided by " + sceneDatas.GetScale()
            + " :\n" + volumeMesh.ToString("F2") + " m3\nReal volume :\n" + (sceneDatas.GetScale() * volumeMesh).ToString("F2") + "m3";
        }
        else
        {
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume :\n" + volumeMesh.ToString("F2") + " m3";
        }
    }

    private void MeshHandler()
    {
        if (Checkpoints() && !sceneDatas.IsMeshCreated() && sceneDatas.IsPointsPlaced())
        {
            List<Vector3> tmp = new List<Vector3>();
            for (int i = 0; i < sceneDatas.GetVerticesSize(); i ++)
            {
                if (i%2 == 0)
                {
                    tmp.Add(new Vector3(sceneDatas.GetVertices()[i].x,
                                        sceneDatas.GetVertices()[i].y,
                                        sceneDatas.GetVertices()[i].z));
                }
                else
                {
                    tmp.Add(new Vector3(sceneDatas.GetVertices()[i].x,
                                        sceneDatas.GetVertices()[i].y + currentOffset,
                                        sceneDatas.GetVertices()[i].z));
                }
            }

            mesh.vertices = tmp.ToArray();
            CreateTriangles(tmp);

            VolumeMeshCalcul();
            if (GameObject.Find("WaterVolumeText"))
            {
                UpdateVolumeText();
            }
            mesh.triangles = triangles.ToArray();

            mesh.MarkDynamic();
            mesh.Optimize();
            mesh.OptimizeIndexBuffers();
            mesh.OptimizeReorderVertexBuffer();

            go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            go.GetComponent<MeshRenderer>().material = Resources.Load("WaterURP", typeof(Material)) as Material;
            go.GetComponent<MeshFilter>().mesh = mesh;

            sceneDatas.SetMeshCreated(true);
        }

        else if(Checkpoints() && sceneDatas.IsMeshCreated())
        {
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }
    }
}
