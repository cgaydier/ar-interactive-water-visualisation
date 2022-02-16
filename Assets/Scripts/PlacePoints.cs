using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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
    public int nb_vertices = 0;
    // Update is called once per frame

    //private void Start()
    //{
    //    Vector3 pos = new Vector3(0, 0, 0);
    //    Quaternion rot = Quaternion.identity;
    //    points.Add(Instantiate(m_PointToPlace, pos, rot));
    //    points.Add(Instantiate(m_PointToPlace, pos, rot));
    //    points.Add(Instantiate(m_PointToPlace, pos, rot));
    //    points.Add(Instantiate(m_PointToPlace, pos, rot));
    //    points.Add(Instantiate(m_PointToPlace, pos, rot));
    //    points.Add(Instantiate(m_PointToPlace, pos, rot));
    //    vertices.Add(pos);
    //    vertices.Add(pos);
    //    vertices.Add(pos);
    //    vertices.Add(pos);
    //    vertices.Add(pos);
    //    vertices.Add(pos);
    //    nb_vertices = 6;
    //}

    void Update()
    {
        if(Input.touchCount > 0 && enumState.GetState() == EnumState.State.PlacePoints)
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began)
            {
                if(m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;
                
                    points.Add(Instantiate(m_PointToPlace, hitPose.position, hitPose.rotation));
                    vertices.Add(hitPose.position);
                    nb_vertices++;
                }
            }
        }
    }
}
