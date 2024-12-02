using System;
using System.Collections;
using System.Collections.Generic;
using Exterior;
using UnityEngine;

namespace Exterior
{
    public class CameraVissionLimitsDetectionMax : MonoBehaviour
    {
        public bool isInVissionRange;
        public void OnTriggerEnter(Collider other)
        {
            isInVissionRange = true;
        }
        public void OnTriggerExit(Collider other)
        {
            isInVissionRange = false;
        }
    }
}

