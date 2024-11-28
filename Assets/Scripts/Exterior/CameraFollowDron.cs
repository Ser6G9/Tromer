using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Exterior
{
    public class CameraFollowDron : MonoBehaviour
    {
        private TromerLevelManager levelManager;
        private CameraVissionLimitsDetection cameraVissionLimitsDetection;
        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
            cameraVissionLimitsDetection = GameObject.FindGameObjectWithTag("VissionLimitLeft").GetComponent<CameraVissionLimitsDetection>();
        } // Intenta recoger el valor de si está o no dentro del sensor!!!

        public float rotationSpeed;
        public float xInclinationCamera;
        public float maxYRotation;
        public float minYRotation;
        
        public float yRotationInAnteriorFrame;
        public float deltaYRotation;
        public float targetCurrentYAngle;

        private void Start()
        {
            LookAtTarget(levelManager.dron);
        }

        void Update()
        {
            LookAtTarget(levelManager.dron);
        }
        
        public void LookAtTarget(GameObject target)
        {
            // Se calcula la dirección hacia el target en cuestión
            Vector3 directionToTarget = target.transform.position - transform.position;
            directionToTarget.y = 0;
            
            // Se aplica la rotación de la cámara hacia la posición actual del target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            deltaYRotation = targetRotation.eulerAngles.y - yRotationInAnteriorFrame;
            targetCurrentYAngle += deltaYRotation;
            yRotationInAnteriorFrame = targetCurrentYAngle;
            deltaYRotation = 0;
            
            // La cámara rotará solo si el target se encuentra en su radio de visión
            if (targetCurrentYAngle >= minYRotation && targetCurrentYAngle <= maxYRotation)
            { 
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(xInclinationCamera, targetCurrentYAngle, 0), rotationSpeed * Time.deltaTime);
            }
        }
    }
}

