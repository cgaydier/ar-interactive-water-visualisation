using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacePoints : MonoBehaviour
{
    SceneDatas sceneDatas;
    GraphicRaycaster GR;

    public ARRaycastManager m_RaycastManager;
    public GameObject m_PointToPlace;
    public EnumState enumState;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    public List<GameObject> points = new List<GameObject>();
    private List<Vector3> lines = new List<Vector3>();
    
    private LineRenderer LR;
    private bool first;

    private void Start()
    {
        GR = GameObject.Find("UICanvas").GetComponent<GraphicRaycaster>();
        sceneDatas = GameObject.Find("SceneDatas").GetComponent<SceneDatas>();
        first = true;
    }

    public void ClearAll()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Destroy(points[i]);
        }
        first = true;
        lines.Clear();
        sceneDatas.vertices.Clear();
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

                    if (results.Count == 0)
                    {
                        Pose hitPose = s_Hits[0].pose;

                        points.Add(Instantiate(m_PointToPlace, hitPose.position, hitPose.rotation));
                        if(first){
                            LR = points[0].AddComponent<LineRenderer>();
                            Material lineMat = Resources.Load("Line", typeof(Material)) as Material;
                            LR.material = lineMat;
                            LR.widthMultiplier = 0.008f;
                            LR.positionCount = 0;
                            first = false;
                        }
                        LR.positionCount = points.Count;
                        sceneDatas.vertices.Add(hitPose.position);
                        lines.Add(hitPose.position);
                        sceneDatas.vertices.Add(hitPose.position);
                        
                        LR.SetPositions(lines.ToArray());
                        LR.loop = true;
                    }
                }
            }
        }
    }
}