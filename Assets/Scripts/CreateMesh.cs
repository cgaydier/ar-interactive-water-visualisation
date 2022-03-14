using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
    public GameObject content;
    private SceneData sceneData;
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
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        volumeMesh = 0f;
        offset = sceneData.GetDefaultOffset();
        currentOffset = sceneData.GetDefaultOffset();
        //sceneData.AddVertice(new Vector3(0, 0, 0));
        //sceneData.AddVertice(new Vector3(0, 0, 0));
        //sceneData.AddVertice(new Vector3(1, 0, 0));
        //sceneData.AddVertice(new Vector3(1, 0, 0));
        //sceneData.AddVertice(new Vector3(1, 0, 1));
        //sceneData.AddVertice(new Vector3(1, 0, 1));
        //sceneData.AddVertice(new Vector3(0, 0, 1));
        //sceneData.AddVertice(new Vector3(0, 0, 1));
        //sceneData.SetPointsPlaced(true);
    }

    /* summary :
    * Updates the mesh at each frame
    */
    void Update()
    {
        MeshHandler();
    }

    /* summary :
    * Increases the value with which the volume will be divided
    */
    public void AddScale()
    {
        sceneData.IncrScale();
        SetWater();
    }

    public void DecrScale()
    {
        SetWater();
    }

    /* summary :
    * Calculates a height value depending on the surface
    */
    public void AddWater(float volume)
    {
        offset += (volume / sceneData.GetSurfaceMesh());
        SetWater();
    }

    public void RemoveWater(float volume)
    {
        offset = offset - (volume / sceneData.GetSurfaceMesh()) >= 0.00f ? offset - (volume / sceneData.GetSurfaceMesh()) : 0.00f;
        SetWater();
    }

    public void SetCustomVolume(float volume){
        if(volume >= 0f)
        {
            offset = (volume / sceneData.GetSurfaceMesh());
            SetWater();
        }
    }

    /* summary :
    * Changes the current value of the volume height depending on the time scale
    */
    public void SetWater()
    {
        switch (sceneData.currentTime)
        {
            case SceneData.TimeName.Day:
                currentOffset = offset / 7f;
                break;

            case SceneData.TimeName.Week:
                currentOffset = offset;
                break;

            case SceneData.TimeName.Month:
                currentOffset = offset * 4;
                break;

            case SceneData.TimeName.Year:
                currentOffset = offset * 52;
                break;

            default:
                Debug.Log("Unknown Time type !" + sceneData.currentTime);
                break;
        }
        currentOffset /= (float)sceneData.GetScale();
        RefreshMesh();
    }

    /* summary :
    * Resets the height of the volume while keeping the surface intact
    */
    public void Reset()
    {
        if(sceneData.IsMeshCreated())
        {
            currentOffset = sceneData.GetDefaultOffset();
            offset = sceneData.GetDefaultOffset();
            RefreshMesh();
        }
        else
        {
            errorHandler.NoVolumeError();
        }
    }

    /* summary :
    * Recalculates the volume mesh with a different height's value
    */
    private void RefreshMesh()
    {
        sceneData.SetSurfaceMesh(0f);
        volumeMesh = 0f;
        triangles.Clear();
        mesh.Clear();
        sceneData.SetMeshCreated(false);
        Destroy(go);
        lineToReset = true;
    }

    public void ClearAll()
    {
        RefreshMesh();
        offset = sceneData.GetDefaultOffset();
        currentOffset = sceneData.GetDefaultOffset();
        sceneData.SetPointsPlaced(false);
        if (GameObject.Find("WaterVolumeText"))
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume :\n0 L";
    }

    /* summary :
    * Creates a triangular mesh by extruding the user-made surface
    */
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
            sceneData.SetSurfaceMesh(sceneData.GetSurfaceMesh() + ((Vector3.Distance(vertices[0], vertices[j+2]) * Vector3.Distance(vertices[j+2], vertices[j+4])) / 2f));

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
    
    /* summary :
    * Calculates a volume value in liter
    */
    private void VolumeMeshCalcul()
    {
        volumeMesh = sceneData.GetSurfaceMesh() * currentOffset * 1000;
    }

    /* summary :
    * Checks if the number of placed points is valide
    */
    private bool Checkpoints()
    {
        int nbTotal = (sceneData.GetVerticesSize()) / 2;
        if (nbTotal < sceneData.GetMinPoints() || nbTotal > sceneData.GetMaxPoints())
            return false;
        return true;
    }

    /* summary :
    * Updates the volume value shown
    */
    public void UpdateVolumeText()
    {
        if (sceneData.GetScale() > 1)
        {
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Visible volume divided by " + sceneData.GetScale()
            + " :\n" + volumeMesh.ToString("F0") + " L\nReal volume :\n" + (sceneData.GetScale() * volumeMesh).ToString("F0") + "L";
        }
        else
        {
            GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Volume :\n" + volumeMesh.ToString("F0") + " L";
        }
    }

    /* summary :
    * Updates the mesh, the volume value and apply the calculated height to it if a change has been made by the user
    */
    private void MeshHandler()
    {
        if (Checkpoints() && !sceneData.IsMeshCreated() && sceneData.IsPointsPlaced())
        {
            List<Vector3> tmp = new List<Vector3>();
            // Recalculates the dupplicated vertices to a custom height to create a volume
            for (int i = 0; i < sceneData.GetVerticesSize(); i ++)
            {
                if (i%2 == 0)
                {
                    tmp.Add(new Vector3(sceneData.GetVertices()[i].x,
                                        sceneData.GetVertices()[i].y,
                                        sceneData.GetVertices()[i].z));
                }
                else
                {
                    tmp.Add(new Vector3(sceneData.GetVertices()[i].x,
                                        sceneData.GetVertices()[i].y + currentOffset,
                                        sceneData.GetVertices()[i].z));
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

            // Reapplies the water material to the updated mesh
            go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            go.GetComponent<MeshRenderer>().material = Resources.Load("WaterURP", typeof(Material)) as Material;
            go.GetComponent<MeshFilter>().mesh = mesh;

            sceneData.SetMeshCreated(true);

            if (lineToReset)
            {
                content.GetComponent<ScrollButtonFunctions>().ResetConsumption();
                lineToReset = false;
            }
        }
        // Updates the lighting on the volume if there isn't any changes impacting its size
        else if(Checkpoints() && sceneData.IsMeshCreated())
        {
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }
    }
}
