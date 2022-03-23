using NUnit.Framework;
using UnityEngine;

public class MeshHandlerButtonsTests
{
    private MeshHandlerButtons meshHandlerButtons;

    private void StartFunction()
    {
        meshHandlerButtons = GameObject.Find("MeshHandler").GetComponent<MeshHandlerButtons>();
        meshHandlerButtons.Start();
    }

    [Test]
    public void CreatePointsMeshTest()
    {
    }

    [Test]
    public void ValidatePointsMeshTest()
    {
    }

    [Test]
    public void ClearPointsMeshTest()
    {
    }

    [Test]
    public void CreateArbitraryMeshTest()
    {
    }

    [Test]
    public void ResetMeshSettingsTest()
    {
    }

    [Test]
    public void ClearSettingsTest()
    {
    }
}
