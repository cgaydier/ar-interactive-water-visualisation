using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMesh : MonoBehaviour
{
    void ClearM()
    {
        Destroy(GameObject.Find("Mesh"));
    }
}
