using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Exterior
{
    public class VissionCampDetectPresence : MonoBehaviour
    {
        private TromerLevelManager levelManager;
        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
        }

        public GameObject camera;
        public float cameraRotationSpeed;
        public float maxCameraYRotation;
        public float minCameraYRotation;
        
        public bool isInVissionCamp = false;
        public GameObject target;
        
        private void OnTriggerEnter(Collider other)
        {
            isInVissionCamp = true;
            target = other.gameObject;
        }
        private void OnTriggerExit(Collider other)
        {
            isInVissionCamp = false;
        }
    
        private void Update()
        {
            if (target == levelManager.dron && isInVissionCamp)
            {
                CameraFollowDron();
            }
        }

        private void CameraFollowDron()
        {
            // Se calcula la dirección hacia el dron
            Vector3 direction = target.transform.position - camera.transform.position;
            direction.y = 0;
            
            // Se calcula la rotación deseada hacia el dron
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            float targetCurrentYAngle = targetRotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(33.5f, targetCurrentYAngle, 0);
            
            // Si el target está dentro del rango, rota la cámara hacia el ángulo deseado
            camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, rotation, cameraRotationSpeed * Time.deltaTime);
        }
    }
}



