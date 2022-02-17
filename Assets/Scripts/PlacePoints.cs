using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class PlacePoints : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    [SerializeField]
    GameObject m_PointToPlace;
    public EnumState enumState;
    public List<GameObject> points = new List<GameObject>();
    public List<Vector3> vertices = new List<Vector3>();
    float offset = 0.2f;

    //private void Start()
    //{
    //    Quaternion rot = Quaternion.identity;
    //    points.Add(Instantiate(m_PointToPlace, new Vector3(0, 0, 0), rot));
    //    points.Add(Instantiate(m_PointToPlace, new Vector3(1, 0, 0), rot));
    //    points.Add(Instantiate(m_PointToPlace, new Vector3(1, 0, 1), rot));
    //    points.Add(Instantiate(m_PointToPlace, new Vector3(0, 0, 1), rot));
    //    points.Add(Instantiate(m_PointToPlace, new Vector3(0, 0, 0), rot));
    //    points.Add(Instantiate(m_PointToPlace, new Vector3(1, 0, 0), rot));
    //    points.Add(Instantiate(m_PointToPlace, new Vector3(1, 0, 1), rot));
    //    points.Add(Instantiate(m_PointToPlace, new Vector3(0, 0, 1), rot));

    //    vertices.Add(new Vector3(0, 0, 0));
    //    vertices.Add(new Vector3(0, 0 + offset, 0));
    //    vertices.Add(new Vector3(1, 0, 0));
    //    vertices.Add(new Vector3(1, 0 + offset, 0));
    //    vertices.Add(new Vector3(1, 0, 1));
    //    vertices.Add(new Vector3(1, 0 + offset, 1));
    //    vertices.Add(new Vector3(0, 0, 1));
    //    vertices.Add(new Vector3(0, 0 + offset, 1));

    //    nb_vertices = 8;
    //}

    public void clearAll()
    {
        for (int i = 0; i < vertices.Count/2; i++)
        {
            print("destroy point : " + i + "\n");
            Destroy(points[i]);
        }
        print("nb vertices avant clear : " + vertices.Count + "\n");
        vertices.Clear();
        print("nb vertices après clear : " + vertices.Count + "\n");
        points.Clear();
    }

    void Update()
    {
        if (Input.touchCount > 0 && enumState.GetState() == EnumState.State.PlacePoints)
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began)
            {
                if(m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {

                    Pose hitPose = s_Hits[0].pose;
                
                    points.Add(Instantiate(m_PointToPlace, hitPose.position, hitPose.rotation));
                    vertices.Add(hitPose.position);
                    vertices.Add(new Vector3(hitPose.position.x, hitPose.position.y + offset, hitPose.position.z));
                }
            }
        }
    }
}
