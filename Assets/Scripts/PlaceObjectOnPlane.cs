using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectOnPlane : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    [SerializeField]
    GameObject m_ObjectToPlace;

    [SerializeField]
    Material mesh_mat;
    Vector3[] vertices = new Vector3[5];
    int[] triangles = new int[100];
    Mesh mesh;    
    GameObject go;
    int cpt = 0;
    bool mesh_created = false;
    void Start()
    {
        mesh = new Mesh();
        go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        Renderer rend = go.GetComponent<Renderer>();
        rend.material = mesh_mat;
        
    }
    public EnumState enumState;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && enumState.GetState() == EnumState.State.MainView)
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began)
            {
                if(m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon) && cpt < 4)
                {
                    Pose hitPose = s_Hits[0].pose;
                
                    Instantiate(m_ObjectToPlace, hitPose.position, hitPose.rotation);
                    vertices[cpt] = hitPose.position;
                    //print("point pos : " + hitPose.position + "\n");
                    print("vertices num " + cpt + " : " + vertices[cpt] + "\n");
                    //print(cpt + "\n");
                    cpt++;
                }
            }
        }

        if(cpt > 3 && !mesh_created){
            CreateMesh();
            mesh_created = true;
            print("mesh creating...\n");
        }

        if(mesh_created){
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            go.transform.position = new Vector3(0f,0f,0f);
            go.GetComponent<MeshFilter>().mesh = mesh;
            //Renderer rend = go.GetComponent<Renderer>();
            //rend.material = mesh_mat;
            
        }
    }
    void CreateMesh()
    {
        triangles = new int[]
        {
            0,1,2,
            0,2,3,
        };
    }
}
