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

        private void Update()
        {
            LocateMarker();
        }

        private void LocateMarker()
        {
            // Marcador del Dron:
            Vector2 dronMarkerPosition = dronMarker.GetComponent<RectTransform>().anchoredPosition;
            dronMarkerPosition.x = ((levelManager.dron.transform.localPosition.z - scenarioInferiorZ) / (scenarioSupperiorZ - scenarioInferiorZ)) * (imgSupperiorX - imgInferiorX) + imgInferiorX;
            dronMarkerPosition.y = ((levelManager.dron.transform.localPosition.x - scenarioInferiorX) / (scenarioSupperiorX - scenarioInferiorX)) * (imgSupperiorY - imgInferiorY) + imgInferiorY;
            dronMarker.GetComponent<RectTransform>().anchoredPosition = dronMarkerPosition;
            
            // Marcador del Enemy:
            Vector2 enemyMarkerPosition = enemyMarker.GetComponent<RectTransform>().anchoredPosition;
            enemyMarkerPosition.x = ((levelManager.enemy.transform.localPosition.z - scenarioInferiorZ) / (scenarioSupperiorZ - scenarioInferiorZ)) * (imgSupperiorX - imgInferiorX) + imgInferiorX;
            enemyMarkerPosition.y = ((levelManager.enemy.transform.localPosition.x - scenarioInferiorX) / (scenarioSupperiorX - scenarioInferiorX)) * (imgSupperiorY - imgInferiorY) + imgInferiorY;
            enemyMarker.GetComponent<RectTransform>().anchoredPosition = enemyMarkerPosition;
        }
    }

}
