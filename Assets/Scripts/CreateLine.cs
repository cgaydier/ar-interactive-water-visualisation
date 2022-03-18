using System.Collections.Generic;
using UnityEngine;

/* summary :
 * Linked to LineHandler
 * Handles the lines for the consumption view
 * 
 * variables :
 * - private - 
 * meshLish - List of lines'meshes
 * goList - List of lines'GameObjects
 * top - Top of the lines 
 * offset - Offset to space out the lines from the mesh
 */
public class CreateLine : MonoBehaviour
{
    private readonly List<Mesh> meshList = new List<Mesh>();
    private readonly List<GameObject> goList = new List<GameObject>();
    private float top = 0f;
    private readonly float offset = 0.02f;

    private void Update()
    {
        for (int i = 0; i < meshList.Count; i++)
        {
            meshList[i].RecalculateNormals();
            meshList[i].RecalculateTangents();
            meshList[i].RecalculateBounds();
        }
    }

    /* summary :
     * Returns the middle of the geometric form created by vertices
     * 
     * parameter :
     * vertices - List of points on space
     * 
     * return :
     * Middle point represented by a Vector3
     */
    private Vector3 GetMiddle(List<Vector3> vertices)
    {
        Vector3 middlePoint = new Vector3(0, 0, 0);

        for (int i = 0; i < vertices.Count; i += 2)
        {
            middlePoint += vertices[i];
        }
        middlePoint /= vertices.Count / 2;

        return middlePoint;
    }

    /* summary :
     * Clears meshList, destroy all the gameObject and set the top of the lines to 0
     */
    public void ClearAll()
    {
        for (int i = 0; i < meshList.Count; i++)
        {
            meshList[i].Clear();
            Destroy(goList[i]);
        }
        top = 0f;
    }

    /* summary :
     * Creates all the triangles for a mesh
     * 
     * parameter :
     * nbTotal - numbers of triangles to create
     * 
     * return :
     * List of the new triangles
     */
    private List<int> CreateTriangles(int nbTotal)
    {
        List<int> triangles = new List<int>();
        for (int i = 0, j = 0; i < nbTotal / 2; i++, j += 2)
        {
            triangles.Add(j % nbTotal);
            triangles.Add((j + 1) % nbTotal);
            triangles.Add((j + 2) % nbTotal);
            triangles.Add((j + 2) % nbTotal);
            triangles.Add((j + 1) % nbTotal);
            triangles.Add((j + 3) % nbTotal);
        }
        return triangles;
    }

    /* summary :
     * Creates a line on top of the others (or 0 if it's the first one)
     * 
     * parameters :
     * thickness - thickness of the new line
     * lineColor - color of the new line
     * vertices - list of vertices for the base of the mesh
     */
    public void AddLine(float thickness, Color lineColor, List<Vector3> vertices)
    {
        AddLine(top, top == 0f ? thickness + 0.001f : thickness, lineColor, vertices);
    }

    /* summary :
     * Creates a new line with the parameters given
     * 
     * parameters :
     * height - height of the base of the mesh
     * thickness - thickness of the new line
     * lineColor - color of the new line
     * vertices - list of vertices for the base of the mesh
     */
    public void AddLine(float height, float thickness, Color lineColor, List<Vector3> vertices)
    {
        Mesh tmpMesh = new Mesh();
        Vector3 middlePoint = GetMiddle(vertices);

        top += thickness;

        List<Vector3> tmp = new List<Vector3>();
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 tmpVector = vertices[i] - middlePoint;
            if (i % 2 == 0)
            {
                tmp.Add(new Vector3(vertices[i].x + offset * tmpVector.x,
                                    vertices[i].y + height,
                                    vertices[i].z + offset * tmpVector.z));
            }
            else
            {
                tmp.Add(new Vector3(vertices[i].x + offset * tmpVector.x,
                                    vertices[i].y + top,
                                    vertices[i].z + offset * tmpVector.z));
            }

        }
        tmpMesh.vertices = tmp.ToArray();
        tmpMesh.triangles = CreateTriangles(vertices.Count).ToArray();
        tmpMesh.MarkDynamic();
        tmpMesh.Optimize();
        tmpMesh.OptimizeIndexBuffers();
        tmpMesh.OptimizeReorderVertexBuffer();

        meshList.Add(tmpMesh);

        GameObject tmpGo = new GameObject("Line", typeof(MeshFilter), typeof(MeshRenderer));
        tmpGo.GetComponent<MeshRenderer>().material = Resources.Load("ConsumptionLines", typeof(Material)) as Material; ;
        Color color = lineColor;
        color.a = 0.8f;
        tmpGo.GetComponent<MeshRenderer>().material.color = color;
        tmpGo.GetComponent<MeshRenderer>().material.renderQueue= 3100;
        tmpGo.GetComponent<MeshFilter>().mesh = tmpMesh;

        goList.Add(tmpGo);
    }
}
