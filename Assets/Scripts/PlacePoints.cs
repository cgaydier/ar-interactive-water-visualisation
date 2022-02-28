using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public GraphicRaycaster GR;

    private void Start()
    {
        GR = GameObject.Find("UICanva").GetComponent<GraphicRaycaster>();
    }

    public void clearAll()
    {
        for (int i = 0; i < vertices.Count/2; i++)
        {
            Destroy(points[i]);
        }
        vertices.Clear();
        points.Clear();
    }

    void Update()
    {
        if (Input.touchCount > 0 && enumState.GetState() == EnumState.State.PlacePoints)
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    PointerEventData ped = new PointerEventData(null)
                    {
                        position = touch.position
                    };
                    List<RaycastResult> results = new List<RaycastResult>();
                    GR.Raycast(ped, results);

                    Debug.Log(results);
                    print(results.Count);
                    if (results.Count == 0)
                    {
                        Pose hitPose = s_Hits[0].pose;

                        points.Add(Instantiate(m_PointToPlace, hitPose.position, hitPose.rotation));
                        vertices.Add(hitPose.position);
                        vertices.Add(hitPose.position);
                    }
                }
            }
        }
    }
}