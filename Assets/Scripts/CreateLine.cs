using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLine : MonoBehaviour
{
    List<Mesh> meshList = new List<Mesh>();
    List<GameObject> goList = new List<GameObject>();
    public Material mesh_mat;

    float thickness = 0.005f;

    private void Update()
    {
        for (int i = 0; i < meshList.Count; i++)
        {
            meshList[i].RecalculateNormals();
            meshList[i].RecalculateTangents();
            meshList[i].RecalculateBounds();
        }
    }

    public void ClearAll()
    {
        for (int i = 0; i < meshList.Count; i++)
        {
            meshList[i].Clear();
            Destroy(goList[i]);
        }
    }

    List<int> CreateTriangles(int nb_total)
    {
        List<int> triangles = new List<int>();
        for (int i = 0, j = 0; i < nb_total / 2; i++, j += 2)
        {
            triangles.Add(j % nb_total);
            triangles.Add((j + 1) % nb_total);
            triangles.Add((j + 2) % nb_total);
            triangles.Add((j + 2) % nb_total);
            triangles.Add((j + 1) % nb_total);
            triangles.Add((j + 3) % nb_total);
        }
        return triangles;
    }
    public void AddLine(float height, List<Vector3> vertices)
    {
        Mesh tmp_mesh = new Mesh();

        height -= thickness / 2f;
        List<Vector3> tmp = new List<Vector3>();
        for (int i = 0; i < vertices.Count; i++)
        {
            if (i % 2 == 0)
            {
                tmp.Add(new Vector3(vertices[i].x,
                                    vertices[i].y + height,
                                    vertices[i].z));
            }
            else
            {
                tmp.Add(new Vector3(vertices[i].x,
                                    vertices[i].y + height + thickness,
                                    vertices[i].z));
            }
        }
        tmp_mesh.vertices = tmp.ToArray();
        tmp_mesh.triangles = CreateTriangles(vertices.Count).ToArray();
        tmp_mesh.MarkDynamic();
        tmp_mesh.Optimize();
        tmp_mesh.OptimizeIndexBuffers();
        tmp_mesh.OptimizeReorderVertexBuffer();
        
        meshList.Add(tmp_mesh);

        GameObject tmp_go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        tmp_go.GetComponent<MeshRenderer>().material = mesh_mat;
        tmp_go.GetComponent<MeshFilter>().mesh = tmp_mesh;

        //tmp_go.transform.localScale += new Vector3(0.05f, 0, 0.05f);

        goList.Add(tmp_go);
    }
}
