using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleARCore;
using GoogleARCore.Examples.CloudAnchors;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject objectToPlace;

    public ARCoreWorldOriginHelper aROrigin;
    private Pose placementPose;
    private bool placementPoseIsValid;
    private bool isPlaced;

    void Start()
    {
        isPlaced = false;
        placementPoseIsValid = false;   
        aROrigin = FindObjectOfType<ARCoreWorldOriginHelper>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (!isPlaced)
        {
            if(placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
                placementIndicator.SetActive(false);
            }
        }
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if(placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);

        }
    }

    private void UpdatePlacementPose()
    {
        Vector3 mScreenCenter = Camera.current.ViewportToScreenPoint(new Vector3(.5f, .5f));
        TrackableHit hit;
        aROrigin.Raycast(mScreenCenter.x, mScreenCenter.y, TrackableHitFlags.Default, out hit);

        if(hit.Pose != null)
        {
            placementPoseIsValid = true;
        }
        
        if (placementPoseIsValid)
        {
            placementPose = hit.Pose;

            Vector3 camForward = Camera.current.transform.forward;
            Vector3 camBearing = new Vector3(camForward.x, 0, camForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(camBearing);
        }
    }
}
