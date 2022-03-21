using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CreateMeshTests
{
    private CreateMesh createMesh;
    private SceneData sceneData;

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
    public void UpdateVolumeTextTest()
    {
        StartFunction();

        sceneData.IncrScale();
        GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text = "Changed for testing\n";

        createMesh.UpdateVolumeText();

        Assert.AreEqual(GameObject.Find("WaterVolumeText").GetComponent<UnityEngine.UI.Text>().text, "Visible volume divided by " + sceneData.GetScale()
            + " :\n" + createMesh.GetVolumeMesh().ToString("F0") + " L\nReal volume :\n" + (sceneData.GetScale() * createMesh.GetVolumeMesh()).ToString("F0") + "L");
    }
}
