using NUnit.Framework;
using UnityEngine;

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

        legendButtonFunctions.OpenAndCloseLegend();

        isActive = legendButtonFunctions.GetIsActive();
        Assert.IsTrue(isActive == true);
        Assert.IsTrue(legendButtonFunctions.SubMenu.activeSelf == true);

        legendButtonFunctions.OpenAndCloseLegend();

        if (!isActive)
        {
            Assert.IsTrue(legendButtonFunctions == null);
        }
    }
}
