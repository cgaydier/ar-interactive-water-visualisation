using System.Collections.Generic;
using UnityEngine;

public class SceneDatas : MonoBehaviour
{
    public EnumState enumState;
    public enum DataName
    {
        Shower,
        Bath,
        HandDish,
        DishWasher,
        WashingMachine,
        Bathroom
    }

    private readonly List<float> dataConsumption = new List<float>
    {
        0.06f,
        0.15f,
        0.017f,
        0.014f,
        0.07f,
        0.009f
    };

    private readonly List<Color> dataColor = new List<Color>
    {
        Color.blue,
        Color.red,
        Color.cyan,
        Color.green,
        Color.magenta,
        Color.yellow
    };

    private List<int> datas = new List<int>();
    private int scale = 1;
    private bool meshCreated = false;
    private bool pointsPlaced = false;
    private bool linesShowned = false;
    private readonly int minPoints = 3;
    private readonly int maxPoints = 10;
    private readonly float defaultOffset = 0.0001f;
    private float surfaceMesh = 0f;
    private List<Vector3> vertices = new List<Vector3>();


    public void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(DataName)).Length; i++)
        {
            datas.Add(0);
        }
    }

    public void ClearAll()
    {
        datas.Clear();
        for (int i = 0; i < System.Enum.GetValues(typeof(DataName)).Length; i++)
        {
            datas.Add(0);
        }

        scale = 1;
        meshCreated = false;
        pointsPlaced = false;
        linesShowned = false;
        surfaceMesh = 0f;
        ClearVertices();
    }

    public void ClearVertices()
    {
        vertices.Clear();
    }

    public float GetDataConsumption(DataName data)
    {
        return dataConsumption[(int)data];
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
        scale --;
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
