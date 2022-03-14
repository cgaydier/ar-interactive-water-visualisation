using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
    public GameObject content;
    private SceneDatas sceneDatas;
    private Mesh mesh;
    private GameObject go;
    private ErrorHandler errorHandler;
    private readonly List<int> triangles = new List<int>();
    private float volumeMesh;
    private float offset;
    private float currentOffset;
    private bool lineToReset = false;

    void Start()
    {
        mesh = new Mesh();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        volumeMesh = 0f;
        offset = sceneDatas.GetDefaultOffset();
        currentOffset = sceneDatas.GetDefaultOffset();
        //sceneDatas.AddVertice(new Vector3(0, 0, 0));
        //sceneDatas.AddVertice(new Vector3(0, 0, 0));
        //sceneDatas.AddVertice(new Vector3(1, 0, 0));
        //sceneDatas.AddVertice(new Vector3(1, 0, 0));
        //sceneDatas.AddVertice(new Vector3(1, 0, 1));
        //sceneDatas.AddVertice(new Vector3(1, 0, 1));
        //sceneDatas.AddVertice(new Vector3(0, 0, 1));
        //sceneDatas.AddVertice(new Vector3(0, 0, 1));
        //sceneDatas.SetPointsPlaced(true);
    }

    // Updates the mesh at each frame
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

    // Calculates a height value depending on the surface
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

    // Changes the current value of the volume height depending on the time scale
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

    // Resets the height of the volume while keeping the surface intact
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

    // Recalculates the volume mesh with a different height's value
    private void RefreshMesh()
    {
        sceneDatas.SetSurfaceMesh(0f);
        volumeMesh = 0f;
        triangles.Clear();
        mesh.Clear();
        sceneDatas.SetMeshCreated(false);
        Destroy(go);
        lineToReset = true;
    }

    public void ClearAll()
    {
        RefreshMesh();
        offset = sceneDatas.GetDefaultOffset();
        currentOffset = sceneDatas.GetDefaultOffset();
        sceneDatas.SetPointsPlaced(false);
        if (GameObject.Find("WaterVolumeText"))
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume :\n0 L";
    }

    // Creates a triangular mesh by extruding the user-made surface
    private void CreateTriangles(List<Vector3> vertices)
    {
        int nbTotal = vertices.Count;

        // Horizontal
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
    
    // Calculates a volume value in liter
    private void VolumeMeshCalcul()
    {
        volumeMesh = sceneDatas.GetSurfaceMesh() * currentOffset * 1000;
    }

    // Checks if the number of placed points is valide
    private bool Checkpoints()
    {
        int nbTotal = (sceneDatas.GetVerticesSize()) / 2;
        if (nbTotal < sceneDatas.GetMinPoints() || nbTotal > sceneDatas.GetMaxPoints())
            return false;
        return true;
    }

    // Updates the volume value shown
    public void UpdateVolumeText()
    {
        if (sceneDatas.GetScale() > 1)
        {
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Visible volume divided by " + sceneDatas.GetScale()
            + " :\n" + volumeMesh.ToString("F0") + " L\nReal volume :\n" + (sceneDatas.GetScale() * volumeMesh).ToString("F0") + "L";
        }
        else
        {
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume :\n" + volumeMesh.ToString("F0") + " L";
        }
    }

    // Updates the mesh, the volume value and apply the calculated height to it if a change has been made by the user
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

            if (lineToReset)
            {
                content.GetComponent<ScrollButtonFunctions>().ResetConsumption();
                lineToReset = false;
            }
        }

        else if(Checkpoints() && sceneDatas.IsMeshCreated())
        {
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }
    }
}
