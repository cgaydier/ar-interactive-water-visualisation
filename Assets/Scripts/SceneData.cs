using System.Collections.Generic;
using UnityEngine;

/* summary : 
 * Linked to SceneData
 * Handles the data of the scene
 * 
 * variables : 
 * - public -
 * TimeName - Enum to set the different timelines for the display
 * DataName - Enum to set the different data's names
 * ExampleName - Enum to set the differents example's names
 * 
 * - private -
 * dataConsumption - List of the average water consumptions for each data in m3
 * exampleConsumption - List of the average water consumptions for each example in m3
 * dataColor - List of the colors for each data
 * enumState - Link to SceneState's script
 * currentTime - Current time for the scale
 * datas - List of data's counter
 * scale - Current scale
 * meshCreated - Used to know if the mesh is created
 * pointsPlaced - Used to know if the points are placed
 * linesShowned - Used to know if the consumption's lines are showned
 * minPoints - Minimum of points to place
 * maxPoints - Maximum of points to place
 * defaultOffset - Default offset for reset
 * surfaceMesh - Current surface of the mesh in m2
 * vertices - List of current mesh vertices
 */
public class SceneData : MonoBehaviour
{
    public enum TimeName
    {
        Day,
        Week,
        Month,
        Year
    }

    public enum DataName
    {
        Bathroom,
        WashingMachine,
        DishWasher,
        HandDish,
        Bath,
        Shower
    }

    private readonly List<float> dataConsumption = new List<float>
    {
        0.009f,
        0.07f,
        0.014f,
        0.017f,
        0.15f,
        0.06f
    };

    public enum ExampleName
    {
        Average1P1W,
        AutoWash,
        ToiletFlushLeak1W,
        DripTap1W,
        Garden1W
    }

    private readonly List<float> exampleConsumption = new List<float>
    {
        1.03f,
        0.2f,
        4.2f,
        0.67f,
        0.38f
    };

    private readonly List<Color> dataColor = new List<Color>
    {
        Color.yellow,
        Color.magenta,
        Color.green,
        Color.cyan,
        Color.red,
        Color.blue
    };

    private EnumState enumState;
    private TimeName currentTime;
    private List<int> datas = new List<int>();
    private int scale;
    private bool meshCreated;
    private bool pointsPlaced;
    private bool linesShowned;
    private readonly int minPoints = 3;
    private readonly int maxPoints = 10;
    private readonly float defaultOffset = 0.0001f;
    private float surfaceMesh;
    private List<Vector3> vertices = new List<Vector3>();

    public void Start()
    {
        enumState = GameObject.Find("SceneState").GetComponent<EnumState>();
        datas.Clear();
        for (int i = 0; i < System.Enum.GetValues(typeof(DataName)).Length; i++)
        {
            datas.Add(0);
        }
        scale = 1;
        currentTime = TimeName.Week;
        meshCreated = false;
        pointsPlaced = false;
        linesShowned = false;
        surfaceMesh = 0f;
        vertices.Clear();
    }

    /* summary :
    * Clears all the counters in the settings interface
    */
    public void ClearCpt()
    {
        datas.Clear();
        for (int i = 0; i < System.Enum.GetValues(typeof(DataName)).Length; i++)
        {
            datas.Add(0);
        }
        scale = 1;
        currentTime = TimeName.Week;
    }

    /* summary :
     * Clears the boolean
     * Calls ClearCpt()
     * Sets surface to 0
     * Calls ClearVertices()
     */
    public void ClearAll()
    {
        ClearCpt();
        meshCreated = false;
        pointsPlaced = false;
        linesShowned = false;
        surfaceMesh = 0f;
        ClearVertices();
    }

    /* summary :
     * Clears all the vertices
     */
    public void ClearVertices()
    {
        vertices.Clear();
    }

    public EnumState GetEnumState()
    {
        return enumState;
    }

    public TimeName GetCurrentTime()
    {
        return currentTime;
    }

    public void SetCurrentTime(TimeName tmp)
    {
        currentTime = tmp;
    }

    public float GetDataConsumption(DataName data)
    {
        return dataConsumption[(int)data];
    }

    public float GetExampleConsumption(ExampleName tmp)
    {
        return exampleConsumption[(int)tmp];
    }

    public Color GetDataColor(DataName data)
    {
        return dataColor[(int)data];
    }

    public void IncrDataCpt(DataName data)
    {
        datas[(int)data]++;
    }

    public bool DecrDataCpt(DataName data)
    {
        if (datas[(int)data] <= 0)
            return false;
        datas[(int)data]--;
        return true;
    }

    public int GetDataCpt(DataName data)
    {
        return datas[(int)data];
    }

    public void IncrScale()
    {
        scale++;
    }

    public bool DecrScale()
    {
        if (scale <= 1)
            return false;
        scale--;
        return true;
    }

    public int GetScale()
    {
        return scale;
    }

    public bool IsMeshCreated()
    {
        return meshCreated;
    }

    public void SetMeshCreated(bool tmp)
    {
        meshCreated = tmp;
    }

    public bool IsPointsPlaced()
    {
        return pointsPlaced;
    }

    public void SetPointsPlaced(bool tmp)
    {
        pointsPlaced = tmp;
    }
    public bool IsLinesShowned()
    {
        return linesShowned;
    }

    public void SetLinesShowned(bool tmp)
    {
        linesShowned = tmp;
    }

    public int GetMinPoints()
    {
        return minPoints;
    }

    public int GetMaxPoints()
    {
        return maxPoints;
    }

    public float GetDefaultOffset()
    {
        return defaultOffset;
    }

    public float GetSurfaceMesh()
    {
        return surfaceMesh;
    }

    public void SetSurfaceMesh(float tmp)
    {
        surfaceMesh = tmp;
    }

    public void AddVertice(Vector3 tmp)
    {
        vertices.Add(tmp);
    }

    public List<Vector3> GetVertices()
    {
        return new List<Vector3>(vertices);
    }

    public int GetVerticesSize()
    {
        return vertices.Count;
    }
}
