using System.Collections.Generic;
using UnityEngine;

public class SceneDatas : MonoBehaviour
{
    private List<int> datas = new List<int>();
    private float scale = 1f;
    public bool meshCreated = false;
    public bool pointsPlaced = false;
    public bool linesShowned = false;
    public int minPoints = 3;
    public int maxPoints = 10;
    public float defaultOffset = 0.0001f;
    public float surfaceMesh = 0f;
    public List<Vector3> vertices = new List<Vector3>();

    public enum datasName 
    {
        Shower, 
        Bath,
        HandDish,
        DishWasher,
        WashingMachine,
        Bathroom
    }

    public List<float> dataConsumption = new List<float>
    {
        0.06f,
        0.15f,
        0.017f,
        0.014f,
        0.07f,
        0.009f
    };

    public List<Color> datasColors = new List<Color>
    {
        Color.blue,
        Color.red,
        Color.cyan,
        Color.green,
        Color.magenta,
        Color.yellow
    };

    public void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(datasName)).Length; i++)
        {
            datas.Add(0);
        }
    }

    public void IncrData(datasName data)
    {
        datas[(int)data]++;
    }
    
    public void DecrData(datasName data)
    {
        datas[(int)data]--;
    }

    public float GetDataConsumption(datasName data)
    {
        return dataConsumption[(int)data];
    }

    public int GetDataCpt(datasName data)
    {
        return datas[(int)data];
    }

    public void IncrScale()
    {
        scale++;
    }

    public void DecrScale()
    {
        scale = scale - 1f > 1f ? scale - 1f : 1f;
    }

    public float GetScale()
    {
        return scale;
    }
}
