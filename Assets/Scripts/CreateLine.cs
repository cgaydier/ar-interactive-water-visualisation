using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLine : MonoBehaviour
{
    List<GameObject> goList = new List<GameObject>();
    public Material meshMat;
    private float offset = 0.2f;

    public void ClearAll()
    {
        for (int i = 0; i < goList.Count; i++)
        {
            Destroy(goList[i]);
        }
    }

    private Vector3 GetMiddle(List<Vector3> vertices)
    {
        Vector3 middlePoint = new Vector3(0, 0, 0);

        for (int i = 0; i < vertices.Count; i+=2)
        {
            middlePoint += vertices[i];
        }
        middlePoint /= vertices.Count / 2;

        return middlePoint;
    }
    public void AddLine(float height, List<Vector3> vertices)
    {
        Vector3 middlePoint = GetMiddle(vertices);

        GameObject tmpGo = new GameObject("Line");
        tmpGo.transform.position = vertices[0];
        tmpGo.AddComponent<LineRenderer>();

        LineRenderer lr = tmpGo.GetComponent<LineRenderer>();
        lr.material = meshMat;
        lr.positionCount = vertices.Count / 2;
        lr.loop = true;
        lr.startWidth = 0.005f;
        for (int i = 0, j = 0; i < vertices.Count; i+=2, j++)
        {
            Vector3 tmp = vertices[i] - middlePoint;
            lr.SetPosition(j, new Vector3(vertices[i].x + offset * tmp.x, 
                                         (vertices[i].y + offset * tmp.y) + height,
                                          vertices[i].z + offset * tmp.z));
        }
        goList.Add(tmpGo);
    }
}
