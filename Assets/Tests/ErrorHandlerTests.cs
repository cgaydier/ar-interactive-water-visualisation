using NUnit.Framework;
using UnityEngine;


public class ErrorHandlerTests
{
    private ErrorHandler errorHandler;
    private string debugMessage;
    
    private void StartFunction()
    {
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
        debugMessage = errorHandler.GetDebugMessage();
    }

    // A Test behaves as an ordinary method
    [Test]
    public void PlacePointsErrorTest()
    {
        StartFunction();
        errorHandler.PlacePointsError();
        debugMessage = errorHandler.GetDebugMessage();
        Assert.AreEqual(debugMessage, "Max points reached (10).\nPlease validate or clear.\n");
    }

    [Test]
    public void AlreadyCreatedErrorTest()
    {
        StartFunction();
        errorHandler.AlreadyCreatedError();
        debugMessage = errorHandler.GetDebugMessage();
        Assert.AreEqual(debugMessage, "Points already placed.\nPlease validate or clear.\n");
    }

    [Test]
    public void AlreadyValidatedErrorTest()
    {
        StartFunction();
        errorHandler.AlreadyValidatedError();
        debugMessage = errorHandler.GetDebugMessage();
        Assert.AreEqual(debugMessage, "Surface already validated.\n");
    }

    [Test]
    public void AlreadyClearedErrorTest()
    {
        StartFunction();
        errorHandler.AlreadyClearedError();
        debugMessage = errorHandler.GetDebugMessage();
        Assert.AreEqual(debugMessage, "Nothing to clear.\n");
    }

    [Test]
    public void NoPointsErrorTest()
    {
        StartFunction();
        errorHandler.NoPointsError();
        debugMessage = errorHandler.GetDebugMessage();
        Assert.AreEqual(debugMessage, "Not enough points.\nAt least 3 are needed.\n");
    }

    [Test]
    public void NoVolumeErrorTest()
    {
        StartFunction();
        errorHandler.NoVolumeError();
        debugMessage = errorHandler.GetDebugMessage();
        Assert.AreEqual(debugMessage, "Please create a water volume.\n");
    }

    [Test]
    public void ErrorMessageReset()
    {
        StartFunction();
        errorHandler.ErrorMessageReset();
        debugMessage = errorHandler.GetDebugMessage();
        Assert.AreEqual(debugMessage, "");
    }
}
