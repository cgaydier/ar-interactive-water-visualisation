using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LegendButtonFonctionsTests
{
    private LegendButtonFunctions legendButtonFunctions;
    private ScrollButtonFunctions scrollButtonFunctions;
    private GameObject legend;
    private bool isActive;

    public void StartFunction()
    {
        scrollButtonFunctions = GameObject.Find("Content").GetComponent<ScrollButtonFunctions>();
        legend = scrollButtonFunctions.legendPanel;
        legend.SetActive(true);
        legendButtonFunctions = GameObject.Find("LegendPanel").GetComponent<LegendButtonFunctions>(); 
    }

    [Test]
    public void OpenAndCloseLegendTest()
    {
        StartFunction();
        Assert.IsTrue(legendButtonFunctions != null);

        isActive = legendButtonFunctions.GetIsActive();
        Assert.IsTrue(legendButtonFunctions == true);
        Assert.IsTrue(legendButtonFunctions.SubMenu.activeSelf == true);

        if (!legendButtonFunctions)
        {
            Assert.IsTrue(legendButtonFunctions == null);
        }
    }
}
