using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CreateMeshTests
{
    private CreateMesh createMesh;
    private SceneData sceneData;
    List<Vector3> testVertices = new List<Vector3>();

    private void StartFunction()
    {
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        createMesh.Start();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
    }

    // A Test behaves as an ordinary method
    [Test]
    public void AddWaterTest()
    {
        StartFunction();

        float prevOffset = createMesh.GetOffset();
        float postOffset = createMesh.GetOffset();
        float volume = 0.15f;

        createMesh.AddWater(volume);
        postOffset = createMesh.GetOffset();

        Assert.AreEqual(postOffset, prevOffset + (volume / sceneData.GetSurfaceMesh()));
        sceneData.ClearAll();
    }

    [Test]
    public void RemoveWaterTest()
    {
        StartFunction();

        float prevOffset = createMesh.GetOffset();
        float postOffset = createMesh.GetOffset();
        float volume = 0.15f;

        createMesh.RemoveWater(volume);
        postOffset = createMesh.GetOffset();

        Assert.AreEqual(postOffset, prevOffset - (volume / sceneData.GetSurfaceMesh()) >= 0.00f ? prevOffset - (volume / sceneData.GetSurfaceMesh()) : 0.00f);
        sceneData.ClearAll();
    }

    [Test]
    public void SetCustomVolumeTest()
    {
        StartFunction();

        float postOffset = createMesh.GetOffset();
        float volume = 15000f;

        createMesh.SetCustomVolume(volume);
        postOffset = createMesh.GetOffset();

        Assert.AreEqual(postOffset, volume / sceneData.GetSurfaceMesh());
        sceneData.ClearAll();
    }

    [Test]
    public void SetWaterTest()
    {
        StartFunction();
        
        float offset = createMesh.GetOffset();
        float currentOffset = 0f;
        sceneData.SetCurrentTime(SceneData.TimeName.Day);

        createMesh.SetWater();
        currentOffset = createMesh.GetCurrentOffset();
        
        Assert.AreEqual(currentOffset, (offset/7f) / (float)sceneData.GetScale());

        sceneData.SetCurrentTime(SceneData.TimeName.Week);
        
        createMesh.SetWater();
        currentOffset = createMesh.GetCurrentOffset();
        
        Assert.AreEqual(currentOffset, offset / (float)sceneData.GetScale());

        sceneData.SetCurrentTime(SceneData.TimeName.Month);
        
        createMesh.SetWater();
        currentOffset = createMesh.GetCurrentOffset();
        
        Assert.AreEqual(currentOffset, (offset*4f) / (float)sceneData.GetScale());

        sceneData.SetCurrentTime(SceneData.TimeName.Year);
        
        createMesh.SetWater();
        currentOffset = createMesh.GetCurrentOffset();
        
        Assert.AreEqual(currentOffset, (offset*52f) / (float)sceneData.GetScale());
        sceneData.ClearAll();
    }

    [Test]
    public void ResetTest()
    {
        StartFunction();

        sceneData.SetMeshCreated(true);
        createMesh.SetCurrentOffset(10f);
        createMesh.SetOffset(15f);

        createMesh.Reset();

        Assert.AreEqual(sceneData.GetDefaultOffset(), createMesh.GetCurrentOffset());
        Assert.AreEqual(sceneData.GetDefaultOffset(), createMesh.GetOffset());
        sceneData.ClearAll();
    }

    [Test]
    public void ClearAllTest()
    {
        StartFunction();

        createMesh.SetCurrentOffset(10f);
        createMesh.SetOffset(15f);
        sceneData.SetPointsPlaced(true);
        GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Changed for testing\n";

        createMesh.ClearAll();

        Assert.AreEqual(sceneData.GetDefaultOffset(), createMesh.GetCurrentOffset());
        Assert.AreEqual(sceneData.GetDefaultOffset(), createMesh.GetOffset());
        Assert.AreEqual(GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text, "Volume :\n0 L");
        Assert.IsFalse(sceneData.IsPointsPlaced());
    }

    [Test]
    public void MeshHandlerTest()
    {
        StartFunction();

        sceneData.AddVertice(new Vector3(0f, 0f, 0f));
        sceneData.AddVertice(new Vector3(0f, 0f, 1f));
        sceneData.AddVertice(new Vector3(0f, 1f, 0f));
        sceneData.AddVertice(new Vector3(0f, 1f, 1f));
        sceneData.AddVertice(new Vector3(1f, 0f, 0f));
        sceneData.AddVertice(new Vector3(1f, 0f, 1f));
        sceneData.AddVertice(new Vector3(1f, 1f, 0f));
        sceneData.AddVertice(new Vector3(1f, 1f, 1f));
        sceneData.SetPointsPlaced(true);
        sceneData.SetMeshCreated(false);
        createMesh.SetLineToReset(false);

        createMesh.TestMeshHandler();
        Mesh mesh = createMesh.GetMesh();

        // Test vertices
        Vector3[] vertices = mesh.vertices;
        List<Vector3> tmp = new List<Vector3>();
        for (int i = 0; i < sceneData.GetVerticesSize(); i ++)
        {
            if (i%2 == 0)
            {
                Assert.AreEqual(vertices[i], sceneData.GetVertices()[i]);
            }
            else
            {
                Assert.AreEqual(vertices[i].x, sceneData.GetVertices()[i].x);
                Assert.AreEqual(vertices[i].y, sceneData.GetVertices()[i].y + createMesh.GetCurrentOffset());
                Assert.AreEqual(vertices[i].z, sceneData.GetVertices()[i].z);
            }
        }

        // Test triangles
        int[] triangles = mesh.triangles;
        int[] tmpTriangles = { 0, 4, 2, 1, 5, 3, 0, 6, 4, 1, 7, 5, 0, 1, 2, 2, 1, 3, 2, 3, 4, 4, 3, 5, 4, 5, 6, 6, 5, 7, 6, 7, 0, 0, 7, 1 };
        Assert.AreEqual(triangles, tmpTriangles);

        // Test volume mesh
        float volumeMesh = createMesh.GetVolumeMesh();
        float testVolumeMesh = sceneData.GetSurfaceMesh() * createMesh.GetCurrentOffset() * 1000;
        Assert.AreEqual(volumeMesh,testVolumeMesh);
    }

    [Test]
    public void UpdateVolumeTextTest()
    {
        StartFunction();

        sceneData.IncrScale();
        GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Changed for testing\n";

        createMesh.UpdateVolumeText();


        Assert.AreEqual(GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text, "Visible volume divided by " + sceneData.GetScale()
            + " :\n" + createMesh.GetVolumeMesh().ToString("F0") + " L\nReal volume :\n" + (sceneData.GetScale() * createMesh.GetVolumeMesh()).ToString("F0") + "L");
        sceneData.ClearAll();
    }
}
