using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementController : MonoBehaviour
{
    public GameObject target = null;
    // public GameObject prefab = null;
    public GameObject cam = null;
    public Text modeText = null;
    public float distance = 5f;
    public ARPlacementMode placementMode = ARPlacementMode.PLACE_IN_AIR;
    private ARRaycastManager arRaycastManager = null;
    private bool isLocked = false;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }
    
    private void Update()
    {
        PlaceInAir();
    }

    //private void PlaceFreeAspekt()
    //{
    //    target.transform.position = cam.transform.position + cam.transform.forward * distance;
    //}

    //private void PlaceOnGround()
    //{
    //    if(isLocked) return;

    //    Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    //    if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
    //    {
    //        Pose hitPose = hits[0].pose;
    //        target.transform.position = hitPose.position;
    //    }
    //}

    private void PlaceInAir()
    {
        if(isLocked) return;

        target.transform.position = cam.transform.position + cam.transform.forward * distance;
    }

    public void ChangeLock(ARToolkitController arTool) {
        isLocked = !isLocked;
        if (isLocked)
        {
            arTool.ToggleToolkit();
        }

    }

    public bool GetLock()
    {
        return isLocked;
    }
    public void SetLock(bool locked)
    {
        isLocked = locked;
    }
}
