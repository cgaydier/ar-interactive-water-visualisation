using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class PlacePointsTests
{
    private GraphicRaycaster GR;
    private ErrorHandler errorHandler;
    private SceneData sceneData;
    private PlacePoints placePoints;


    private void StartFunction()
    {
        GR = GameObject.Find("UICanvas").GetComponent<GraphicRaycaster>();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        placePoints = GameObject.Find("MeshHandler").GetComponent<PlacePoints>();
    }

    // A Test behaves as an ordinary method
    [Test]
    public void ClearAllTest()
    {
        StartFunction();

        int nbIncr = 16;
        for(int i = 0; i < nbIncr; i++)
        {
            placePoints.IncrPoints();
            placePoints.IncrLines();
        }
        placePoints.SetFirst(false);
        
        placePoints.ClearAll();

        Assert.AreEqual(placePoints.GetFirst(), true);
        Assert.AreEqual(placePoints.PointsCount(), 0);
        Assert.AreEqual(placePoints.LinesCount(), 0);
    }

    [Test]
    public void PointsCountTest()
    {
        StartFunction();

        int nbIncr = 16;
        for(int i = 0; i < nbIncr; i++)
        {
            placePoints.IncrPoints();
        }
        
        Assert.AreEqual(placePoints.PointsCount(), nbIncr);
    }

    [Test]
    public void PlacePointsHandlerTest()
    {
        
    }
}
