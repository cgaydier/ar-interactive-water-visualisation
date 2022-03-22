using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MenuScriptTests
{
    private MenuScript menuScript;
    private HelpScript helpScript;
    private bool isActive;

    public void StartFunction()
    {
        helpScript = GameObject.Find("TipsPanel").GetComponent<HelpScript>();
        helpScript.OpenAndClosePanel();
        menuScript = GameObject.Find("MenuPanel").GetComponent<MenuScript>();
    }


    [Test]
    public void OpenAndCloseMenuTest()
    {
        StartFunction();
        Assert.IsTrue(menuScript != null);
        isActive = menuScript.GetIsActive();
        Assert.IsTrue(isActive == false);

        menuScript.OpenAndCloseMenu();
    
        isActive = menuScript.GetIsActive();
        Assert.IsTrue(isActive == true);
        Assert.IsTrue(menuScript.subMenu.activeSelf == true);

        menuScript.OpenAndCloseMenu();
        
        if (!helpScript)
        {
            Assert.IsTrue(helpScript == null);
        }

        isActive = menuScript.GetIsActive();
        Assert.IsTrue(isActive == false);
        Assert.IsTrue(menuScript.subMenu.activeSelf == false);
    }
}
