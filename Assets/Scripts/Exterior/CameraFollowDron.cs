using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Exterior
{
    public class CameraFollowDron : MonoBehaviour
    {
        private TromerLevelManager _levelManager;
        private CameraVissionLimitsDetectionMin _cameraVissionLimitsDetectionMin;
        private CameraVissionLimitsDetectionMax _cameraVissionLimitsDetectionMax;
        private void OnEnable()
        {
            _levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
            _cameraVissionLimitsDetectionMin = GameObject.FindGameObjectWithTag("VissionLimitMin").GetComponent<CameraVissionLimitsDetectionMin>();
            _cameraVissionLimitsDetectionMax = GameObject.FindGameObjectWithTag("VissionLimitMax").GetComponent<CameraVissionLimitsDetectionMax>();
        }

        public float rotationSpeed;
        public float xInclinationCamera;
        public float maxYRotation;
        public float minYRotation;
        
        public float yRotationInAnteriorFrame;
        public float deltaYRotation;
        public float targetCurrentYAngle;

        private void Start()
        {
            LookAtTarget(_levelManager.dron);
        }

        void Update()
        {
            // Método para seguir al dron
            LookAtTarget(_levelManager.dron);
            
            // Si el dron entra en el campo de visión de la cámara, aunque no esté centrado, la cámara lo detectará y rotará hacia su posición Minima o Máxima para tener el dron a la vista.
            if (_cameraVissionLimitsDetectionMin.isInVissionRange)
            {
                LookAtMinOrMaxYRotation(minYRotation);
            } else if (_cameraVissionLimitsDetectionMax.isInVissionRange)
            {
                LookAtMinOrMaxYRotation(maxYRotation);
            }
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

        public void LookAtMinOrMaxYRotation(float rotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(xInclinationCamera, rotation, 0), rotationSpeed * Time.deltaTime);
        }
    }
}

