using System.Collections.Generic;
using UnityEngine;

/* summary : 
 * Linked to MeshHandler
 * Handles the mesh creation
 * 
 * variables :
 * - public - 
 * content - Content GameObject
 * arPlane - ar_plane Material - used to make the planes invisible after creating the mesh
 * 
 * - private -
 * sceneData - Link to SceneData's script
 * mesh - Mesh to save the current mesh created
 * go - GameObject to save the current GameObject created
 * errorHandler - Link to ErrorHandler's script 
 * triangles - List of mesh's triangles
 * volumeMesh - Current volume of the mesh
 * offset - Maximal height for a week for the mesh
 * current offset - Current height of the mesh
 * lineToReset - Used to know if the lines for water consumption have to be reset or no
 */
public class CreateMesh : MonoBehaviour
{
    public GameObject content;
    public Material arPlane;
    private SceneData sceneData;
    private Mesh mesh;
    private GameObject go;
    private ErrorHandler errorHandler;
    private readonly List<int> triangles = new List<int>();
    private float volumeMesh;
    private float offset;
    private float currentOffset;
    private bool lineToReset = false;

    public void Start()
    {
        mesh = new Mesh();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        volumeMesh = 0f;
        offset = sceneData.GetDefaultOffset();
        currentOffset = sceneData.GetDefaultOffset();
    }

    void Update()
    {
        MeshHandler();
    }

    /* summary :
     * Adds volume to water
     * 
     * parameter :
     * volume - quantity of water (m3) to add
     */
    public void AddWater(float volume)
    {
        offset += (volume / sceneData.GetSurfaceMesh());
        SetWater();
    }

    /* summary :
     * Removes volume to water
     * 
     * parameter :
     * volume - quantity of water (m3) to remove
     */
    public void RemoveWater(float volume)
    {
        offset = offset - (volume / sceneData.GetSurfaceMesh()) >= 0.00f ? offset - (volume / sceneData.GetSurfaceMesh()) : 0.00f;
        SetWater();
    }

    /* summary :
    * Calculates a height value depending on the surface and the volume value entered by the user
    *
    * parameter :
    * volume - quantity of water in liter
    */
    public void SetCustomVolume(float volume){
        // Converts the volume from liter to m3 for calculation
        volume /= 1000;
        if (volume >= 0f)
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
        switch (sceneData.GetCurrentTime())
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
                Debug.Log("Unknown Time type !" + sceneData.GetCurrentTime());
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
        if (sceneData.IsMeshCreated())
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
    * Recalculates the volume mesh with a different height value
    */
    private void RefreshMesh()
    {
        sceneData.SetSurfaceMesh(0f);
        volumeMesh = 0f;
        triangles.Clear();
        mesh.Clear();
        sceneData.SetMeshCreated(false);
        DestroyImmediate(go);
        lineToReset = true;
    }

    /* summary :
     * Clears all the data
     * Call RefreshMesh()
     * Reset text volume on UI
     */
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
    *
    * parameter :
    * vertices - the list of vertices coordinates organized for mesh creation
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
    * 
    * return :
    * true - if the quantity of points is good
    * false - otherwise
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
    * Sets the planes on the ground transparent or visible
    */
    public void ArPlaneTransparency(bool state)
    {
        Color tmp = new Color(0.3f, 0.3f, 0.3f);
        if (state)
            tmp.a = 0f;
        
        else
            tmp.a = 0.6f;
        
        arPlane.color = tmp;
    }

    /* summary :
    * Updates the mesh, the volume value and apply the calculated height to it if a change has been made by the user
    */
    private void MeshHandler()
    {
        if (Checkpoints() && !sceneData.IsMeshCreated() && sceneData.IsPointsPlaced())
        {
            List<Vector3> tmp = new List<Vector3>();
            // Recalculates the duplicated vertices to a custom height to create a volume
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
            
            // Reapplies the water material to the updated mesh
            go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            go.GetComponent<MeshRenderer>().material = Resources.Load("WaterURP", typeof(Material)) as Material;
            go.GetComponent<MeshFilter>().mesh = mesh;
            sceneData.SetMeshCreated(true);

            // Check if there is lines to reset
            if (lineToReset)
            {
                content.GetComponent<ScrollButtonFunctions>().ResetConsumption();
                lineToReset = false;
            }
        }
        // Updates the lighting on the volume if there isn't any changes impacting its size
        else if (Checkpoints() && sceneData.IsMeshCreated())
        {
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
        }
    }

    /*
    * Test purposes
    */

    public void TestMeshHandler()
    {
        MeshHandler();
    }

    public void SetLineToReset(bool state)
    {
        lineToReset = state;
    }

    public float GetOffset()
    {
        return offset;
    }

    public void SetOffset(float newOffset)
    {
        offset = newOffset;
    }

    public float GetCurrentOffset()
    {
        return currentOffset;
    }

    public void SetCurrentOffset(float newCurrentOffset)
    {
        offset = newCurrentOffset;
    }

    public float GetVolumeMesh()
    {
        return volumeMesh;
    }

    public Mesh GetMesh()
    {
        return mesh;
    }
}
