using System;
using System.Collections;
using System.Collections.Generic;
using Exterior;
using UnityEngine;

public class CameraVissionLimitsDetection : MonoBehaviour
{
    CameraFollowDron CameraFollowDron;
    
    //public GameObject camera;
    
    public bool isInVissionRange;
    public void OnTriggerEnter(Collider other)
    {
        isInVissionRange = true;
    }
    public void OnTriggerExit(Collider other)
    {
        isInVissionRange = false;
    }

    private void Update()
    {
        if (isInVissionRange)
        {
            CameraFollowDron = GameObject.FindGameObjectWithTag("Cam1").GetComponent<CameraFollowDron>(); // NO creo que sea necesario!!!
            CameraFollowDron.LookAtTarget();
        }
        
    }
}
