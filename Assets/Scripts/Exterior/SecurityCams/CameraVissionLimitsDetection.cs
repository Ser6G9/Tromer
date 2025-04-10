using System;
using System.Collections;
using System.Collections.Generic;
using Exterior;
using UnityEngine;

namespace Exterior
{
    public class CameraVissionLimitsDetection : MonoBehaviour
    {
        public bool isInVissionRange;
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Dron")
            {
                isInVissionRange = true;
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.tag == "Dron")
            {
                isInVissionRange = false;
            }
        }
    }
}

