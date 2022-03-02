using System.Collections.Generic;
using UnityEngine;
   

public class CreateLine : MonoBehaviour
{
    List<Mesh> meshList = new List<Mesh>();
    List<GameObject> goList = new List<GameObject>();
    public Material meshMat;

    float thickness = 0.05f;
    float offset = 0.2f;

    private void Update()
    {
        for (int i = 0; i < meshList.Count; i++)
        {
            meshList[i].RecalculateNormals();
            meshList[i].RecalculateTangents();
            meshList[i].RecalculateBounds();
        }
    }

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

    public void ClearAll()
    {
        for (int i = 0; i < meshList.Count; i++)
        {
            meshList[i].Clear();
            Destroy(goList[i]);
        }
    }

    List<int> CreateTriangles(int nbTotal)
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
    public void AddLine(float height, List<Vector3> vertices)
    {
        Mesh tmpMesh = new Mesh();
        Vector3 middlePoint = GetMiddle(vertices);

        height -= thickness / 2f;
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
                                    vertices[i].y + height + thickness,
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
        tmpGo.GetComponent<MeshRenderer>().material = meshMat;
        tmpGo.GetComponent<MeshFilter>().mesh = tmpMesh;

        goList.Add(tmpGo);
    }
}
