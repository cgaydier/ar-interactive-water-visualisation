using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CreateLineTests
{
    private CreateLine createLine;
    List<Vector3> tmp = new List<Vector3>();
    private void StartFunction()
    {
        createLine = GameObject.Find("LineHandler").GetComponent<CreateLine>();
        tmp.Add(new Vector3(0f, 0f, 0f));
        tmp.Add(new Vector3(0f, 0f, 1f));
        tmp.Add(new Vector3(0f, 1f, 0f));
        tmp.Add(new Vector3(0f, 1f, 1f));
        tmp.Add(new Vector3(1f, 0f, 0f));
        tmp.Add(new Vector3(1f, 0f, 1f));
        tmp.Add(new Vector3(1f, 1f, 0f));
        tmp.Add(new Vector3(1f, 1f, 1f));
    }

    [Test]
    public void ClearAllTest()
    {
        StartFunction();

        createLine.AddLine(0.02f, Color.blue, tmp);
        createLine.ClearAll();

        Assert.AreEqual(createLine.GetMeshes().Count, 0);
        Assert.AreEqual(createLine.GetGO().Count, 0);
        Assert.AreEqual(createLine.GetTop(), 0f);
    }

    [Test]
    public void AddLineTest()
    {
        StartFunction();

        createLine.AddLine(0.02f, Color.blue, tmp);
        Assert.AreEqual(createLine.GetMeshes().Count, 1);
        Assert.AreEqual(createLine.GetGO().Count, 1);
        Assert.AreEqual(createLine.GetTop(), 0.021f);

        Mesh tmpMesh = createLine.GetMeshes()[0];

        // Test vertices
        Vector3[] vertices = tmpMesh.vertices;

        Vector3 middle = GetMiddle(tmp);
        Assert.AreEqual(vertices.Length, tmp.Count);

        float offset = 0.02f;
        float top = 0.021f;

        for (int i = 0; i < tmp.Count; i++)
        {
            Vector3 tmpVector = tmp[i] - middle;
            if (i % 2 == 0)
            {
                Assert.AreEqual(vertices[i].x, tmp[i].x + offset * tmpVector.x);
                Assert.AreEqual(vertices[i].y, tmp[i].y);
                Assert.AreEqual(vertices[i].z, tmp[i].z + offset * tmpVector.z);
            }
                
            else
            {
                Assert.AreEqual(vertices[i].x, tmp[i].x + offset * tmpVector.x);
                Assert.AreEqual(vertices[i].y, tmp[i].y + top);
                Assert.AreEqual(vertices[i].z, tmp[i].z + offset * tmpVector.z);
            }
        }

        // Test triangles
        int[] triangles = tmpMesh.triangles;
        int[] tmpTriangles = { 0, 1, 2, 2, 1, 3, 2, 3, 4, 4, 3, 5, 4, 5, 6, 6, 5, 7, 6, 7, 0, 0, 7, 1 };
        Assert.AreEqual(triangles, tmpTriangles);

        // Test GameObject
        GameObject go = createLine.GetGO()[0];

        Assert.AreEqual(go.GetComponent<MeshRenderer>().sharedMaterial.color.r, Color.blue.r);
        Assert.AreEqual(go.GetComponent<MeshRenderer>().sharedMaterial.color.g, Color.blue.g);
        Assert.AreEqual(go.GetComponent<MeshRenderer>().sharedMaterial.color.b, Color.blue.b);
        Assert.AreEqual(go.GetComponent<MeshRenderer>().sharedMaterial.color.a, 0.8f);
        Assert.AreEqual(go.GetComponent<MeshRenderer>().sharedMaterial.renderQueue, 3100);
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
}
