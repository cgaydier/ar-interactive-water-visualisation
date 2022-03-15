using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacePoints : MonoBehaviour
{
    public ARRaycastManager m_RaycastManager;
    public GameObject m_PointToPlace;

    private ErrorHandler errorHandler;
    private SceneData sceneData;
    private GraphicRaycaster GR;
    private static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private readonly List<GameObject> points = new List<GameObject>();
    private readonly List<Vector3> lines = new List<Vector3>();
    private LineRenderer LR;
    private bool first = true;

    private void Start()
    {
        GR = GameObject.Find("UICanvas").GetComponent<GraphicRaycaster>();
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        errorHandler = GameObject.Find("ErrorHandler").GetComponent<ErrorHandler>();
    }

    /* summary :
     * Destroys all the points and clear lines and points
     */
    public void ClearAll()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Destroy(points[i]);
        }
        first = true;
        lines.Clear();
        points.Clear();
    }

    /* summary :
    * Returns the number of placed points
    */
    public int PointsCount()
    {
        return points.Count;
    }

    void Update()
    {
        if (Input.touchCount > 0 && sceneData.GetEnumState().GetState() == EnumState.State.PlacePoints)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                // Launches a ray to detect a surface plane
                if (m_RaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon) && points.Count < sceneData.GetMaxPoints())
                {
                    errorHandler.ErrorMessageReset();
                    PointerEventData ped = new PointerEventData(null)
                    {
                        position = touch.position
                    };

                    List<RaycastResult> results = new List<RaycastResult>();
                    GR.Raycast(ped, results);

                    // Check if user doesn't touch ui
                    if (results.Count == 0)
                    {
                        Pose hitPose = hits[0].pose;

                        // Creates a point on the detected surface
                        points.Add(Instantiate(m_PointToPlace, hitPose.position, hitPose.rotation));

                        // If it's the first intersection Ray/Surface, initialise the lines
                        if (first){
                            LR = points[0].AddComponent<LineRenderer>();
                            Material lineMat = Resources.Load("Line", typeof(Material)) as Material;
                            LR.material = lineMat;
                            LR.widthMultiplier = 0.008f;
                            LR.positionCount = 0;
                            first = false;
                        }

                        // Updates the number of lines and add 2 vertices with the hit coordinates
                        LR.positionCount = points.Count;
                        sceneData.AddVertice(hitPose.position);
                        sceneData.AddVertice(hitPose.position);
                        lines.Add(hitPose.position);

                        LR.SetPositions(lines.ToArray());
                        LR.loop = true;
                    }
                }
                else if (points.Count >= sceneData.GetMaxPoints())
                {
                    errorHandler.PlacePointsError();
                }
            }
        }
    }
}