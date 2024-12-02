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
        private void OnEnable()
        {
            _levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
        }

        public float rotationSpeed;
        public float xInclinationCamera;
        public float maxYRotation;
        public float minYRotation;
        public GameObject vissionLimitMin;
        public GameObject vissionLimitMax;
        
        public float yRotationInAnteriorFrame;
        public float deltaYRotation;
        public float targetCurrentYAngle;

        private void Start()
        {
            LookAtTarget(_levelManager.dron);
        }

        void Update()
        {
            // Método para seguir al dron con la vista.
            LookAtTarget(_levelManager.dron);

            if (vissionLimitMin != null && vissionLimitMax != null)
            {
                // Si el dron entra en el campo de visión de la cámara, aunque no esté centrado, la cámara lo detectará y rotará hacia su posición Minima o Máxima para tener el dron a la vista para el player.
                bool isInMinRange = vissionLimitMin.GetComponent<CameraVissionLimitsDetection>().isInVissionRange;
                bool isInMaxRange = vissionLimitMax.GetComponent<CameraVissionLimitsDetection>().isInVissionRange;
                
                if(isInMinRange)
                { 
                    LookAtMinOrMaxYRotation(minYRotation);
                } else if (isInMaxRange) 
                { 
                    LookAtMinOrMaxYRotation(maxYRotation);
                }
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

