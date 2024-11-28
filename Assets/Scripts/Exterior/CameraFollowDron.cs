using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Exterior
{
    public class CameraFollowDron : MonoBehaviour
    {
        private TromerLevelManager levelManager;
        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
        }
        public float xAxisCamera;
        public float rotationSpeed;
        public float maxYRotation;
        public float minYRotation;
        public Vector3 targetMinPosition;
        public Vector3 targetMaxPosition;
    
        public float targetCurrentYAngle;
        
        void Update()
        {
            // Calcular la dirección hacia el jugador
            Vector3 directionToPlayer = levelManager.dron.transform.position - transform.position;
            directionToPlayer.y = 0;
            
            // Se calcula la rotación deseada hacia el dron
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            targetCurrentYAngle = targetRotation.eulerAngles.y;
            
            targetCurrentYAngle = NormalizeAngle(targetCurrentYAngle);
    
            /*if (targetCurrentYAngle >= targetMinYAngle && targetCurrentYAngle <= targetMaxYAngle)
            {*/
                Quaternion rotation = Quaternion.Euler(xAxisCamera, targetCurrentYAngle, 0);
                if (rotation.eulerAngles.y >= minYRotation && rotation.eulerAngles.y <= maxYRotation)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                }
            //}
            
        }
        
        // Método para normalizar ángulos a un rango [0, 360]
        private float NormalizeAngle(float angle)
        {
            angle = angle % 360;
            if (angle < 0) angle += 360;
            return angle;
        }
    }
}

