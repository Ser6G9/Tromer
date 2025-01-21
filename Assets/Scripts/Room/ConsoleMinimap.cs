using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Room
{
    public class ConsoleMinimap : MonoBehaviour
    {
        private TromerLevelManager levelManager;
        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
        }
        
        public float imgSupperiorX;
        public float imgSupperiorY;
        public float imgInferiorX;
        public float imgInferiorY;
        
        public float scenarioSupperiorX;
        public float scenarioSupperiorZ;
        public float scenarioInferiorX;
        public float scenarioInferiorZ;

        public GameObject dronMarker;
        public GameObject enemyMarker;
        
        public float imgTotalX;
        public float imgTotalY;
        public float scenarioTotalX;
        public float scenarioTotalZ;
        
        public float pixelsPerMetterX;
        public float pixelsPerMetterY;

        private void Start()
        {
            SerialiceVariables();
            CalculatePixelsPerMetterXY();
        }

        private void Update()
        {
            LocateMarker();
        }

        private void LocateMarker()
        {
            // Marcador del Dron:
            Vector2 dronMarkerPosition = dronMarker.GetComponent<RectTransform>().anchoredPosition;
            dronMarkerPosition.y = pixelsPerMetterX * levelManager.dron.transform.position.x;
            dronMarkerPosition.x = pixelsPerMetterY * levelManager.dron.transform.position.z;
            dronMarker.GetComponent<RectTransform>().anchoredPosition = dronMarkerPosition;
            
            /*// Marcador del Enemy:
            Vector3 enemyMarkerPosition = enemyMarker.transform.localPosition;
            enemyMarkerPosition.y = pixelsPerMetterX * levelManager.enemy.transform.position.x;
            enemyMarkerPosition.x = pixelsPerMetterY * levelManager.enemy.transform.position.z;
            enemyMarker.transform.localPosition = enemyMarkerPosition;*/
        }

        private void CalculatePixelsPerMetterXY()
        {
            imgTotalX = imgSupperiorX + imgInferiorX;
            scenarioTotalZ = scenarioSupperiorZ + scenarioInferiorZ;
            pixelsPerMetterX = imgTotalX / scenarioTotalZ;
            
            imgTotalY = imgSupperiorY + imgInferiorY;
            scenarioTotalX = scenarioSupperiorX + scenarioInferiorX;
            pixelsPerMetterY = imgTotalY / scenarioTotalX;
        }

        private void SerialiceVariables()
        {
            if (imgSupperiorX < 0)
            {
                imgSupperiorX *= -1;
            }
            if (imgSupperiorY < 0)
            {
                imgSupperiorY *= -1;
            }
            if (imgInferiorX < 0)
            {
                imgInferiorX *= -1;
            }
            if (imgInferiorY < 0)
            {
                imgInferiorY *= -1;
            }
            
            if (scenarioSupperiorX < 0)
            {
                scenarioSupperiorX *= -1;
            }
            if (scenarioSupperiorZ < 0)
            {
                scenarioSupperiorZ *= -1;
            }
            if (scenarioInferiorX < 0)
            {
                scenarioInferiorX *= -1;
            }
            if (scenarioInferiorZ < 0)
            {
                scenarioInferiorZ *= -1;
            }
        }
    }

}
