using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Room
{
    public class DetectPresence : MonoBehaviour
    {
        public Light light;
        private float lightIntensity = 0.1f;
        private float elapsedTime = 0;
        private void OnTriggerEnter(Collider other)
        {
            light.intensity = 0f;
            light.enabled = true;
            elapsedTime = 0;
        }
        
        private void OnTriggerExit(Collider other)
        {
            light.intensity += 0.1f;
            
        }
    }
}


