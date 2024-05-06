using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class XR_Placement : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<GameObject> spawnedPrefabs;
    private ARRaycastManager raycastManager;
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        spawnedPrefabs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;
            this.ARRaycasting(touchPos);
        } else if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousPos = Input.mousePosition;
            Vector2 mousPos2D = new Vector2(mousPos.x, mousPos.y);
            this.ARRaycasting(mousPos2D);
        }
    }

    private void ARRaycasting(Vector2 pos)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (this.raycastManager.Raycast(pos, hits, TrackableType.PlaneEstimated)) 
        {
            Pose pose = hits[0].pose;
            ARSpawn(pose.position, pose.rotation);
        }
    }
    private void ARSpawn(Vector3 pos, Quaternion rot)
    {
        this.spawnedPrefabs.Add(Instantiate(prefab, pos, rot));
    }
}
