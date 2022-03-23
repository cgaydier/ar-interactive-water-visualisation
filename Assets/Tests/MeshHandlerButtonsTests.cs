using NUnit.Framework;
using UnityEngine;

public class MeshHandlerButtonsTests
{
    private MeshHandlerButtons meshHandlerButtons;
    private SceneData sceneData;
    private PlacePoints placePoints;

    private void StartFunction()
    {
        meshHandlerButtons = GameObject.Find("MeshHandler").GetComponent<MeshHandlerButtons>();
        meshHandlerButtons.Start();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        sceneData.Start();
        placePoints = GameObject.Find("MeshHandler").GetComponent<PlacePoints>();
    }

    [Test]
    public void CreatePointsMeshTest()
    {
        StartFunction();

        Assert.IsFalse(sceneData.IsMeshCreated());
        meshHandlerButtons.CreatePointsMesh();
        Assert.IsFalse(sceneData.IsPointsPlaced());
        Assert.AreEqual(sceneData.GetEnumState().GetState(), EnumState.State.PlacePoints);
    }

    [Test]
    public void ValidatePointsMeshTest()
    {
        StartFunction();

        placePoints.SetPoints(4);
        Assert.IsTrue(placePoints.PointsCount() > sceneData.GetMinPoints());

        meshHandlerButtons.ValidatePointsMesh();

        Assert.AreEqual(sceneData.GetEnumState().GetState(), EnumState.State.MainView);
        Assert.IsTrue(sceneData.IsPointsPlaced());
    }

    [Test]
    public void ClearPointsMeshTest()
    {
        StartFunction();

        sceneData.SetMeshCreated(true);
        meshHandlerButtons.ClearPointsMesh();
        Assert.IsFalse(sceneData.IsPointsPlaced());
        Assert.IsFalse(sceneData.IsMeshCreated());
        Assert.AreEqual(sceneData.GetEnumState().GetState(), EnumState.State.MainView);
        Assert.IsFalse(meshHandlerButtons.settings.activeSelf);
    }
}
