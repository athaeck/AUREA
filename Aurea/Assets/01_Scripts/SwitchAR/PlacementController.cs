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
    public ARPlacementMode placementMode = ARPlacementMode.FREE_ASPEKT;
    private ARRaycastManager arRaycastManager = null;
    private bool isLocked = false;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }
    
    private void Update()
    {
        if (!target || !cam) return;

        switch (placementMode)
        {
            case ARPlacementMode.PLACE_IN_AIR:
                PlaceInAir();
                break;
            case ARPlacementMode.PLACE_ON_GROUND:
                PlaceOnGround();
                break;
            case ARPlacementMode.FREE_ASPEKT:
            default:
                PlaceFreeAspekt();
                break;
        }
    }

    public void ChangeMode() {
        switch (placementMode)
        {
            case ARPlacementMode.PLACE_IN_AIR:
                placementMode = ARPlacementMode.PLACE_ON_GROUND;
                modeText.text = "Place Island on the ground." + isLocked.ToString();
                break;
            case ARPlacementMode.PLACE_ON_GROUND:
                placementMode = ARPlacementMode.FREE_ASPEKT;
                modeText.text = "Free Aspekt." + isLocked.ToString();
                break;
            case ARPlacementMode.FREE_ASPEKT:
            default:
                placementMode = ARPlacementMode.PLACE_IN_AIR;
                modeText.text = "Place Island in the air." + isLocked.ToString();
                break;
        }
    }

    private void PlaceFreeAspekt()
    {
        target.transform.position = cam.transform.position + cam.transform.forward * distance;
    }

    private void PlaceOnGround()
    {
        if(isLocked) return;

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            target.transform.position = hitPose.position;
        }
    }

    private void PlaceInAir()
    {
        if(isLocked) return;

        target.transform.position = cam.transform.position + cam.transform.forward * distance;
    }

    public void ChangeLock() {
        isLocked = !isLocked;
    }

    public bool GetLock()
    {
        return isLocked;
    }
}
