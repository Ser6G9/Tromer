using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

namespace Room
{
    public class ConsoleMinimap : MonoBehaviour
    {
        private GameManager levelManager;
        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<GameManager>();
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
        
        public List<GameObject> taskImgs;
        public List<GameObject> taskMarkers;

        private void Update()
        {
            LocateMarker();
            if (levelManager.dronEnabled)
            {
                dronMarker.GetComponent<Image>().color = new Color(0.8490566f, 0.8272718f, 0.004004993f, 1f);
            }
            else
            {
                dronMarker.GetComponent<Image>().color = new Color(1f, 0.5668886f, 0f, 1f);
            }

            for (int i = 0; i < levelManager.tasksExteriorState.Count; i++)
            {
                if (levelManager.tasksExteriorState[i] == true)
                {
                    taskMarkers[i].GetComponent<Image>().color = new Color(0, 0.83f, 0f, 1f);
                    taskImgs[i].SetActive(false);
                }
                else
                {
                    taskMarkers[i].GetComponent<Image>().color = new Color(1, 0.4048133f, 0f, 1f);
                    taskImgs[i].SetActive(true);
                }
            }
            
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
            
            /*// Marcador de las tareas:
            for (int i = 0; i < tasks.Count; i++)
            {
                Vector2 taskMarkerPosition = taskMarkers[i].GetComponent<RectTransform>().anchoredPosition;
                taskMarkerPosition.x = ((tasks[i].transform.localPosition.z - scenarioInferiorZ) / (scenarioSupperiorZ - scenarioInferiorZ)) * (imgSupperiorX - imgInferiorX) + imgInferiorX;
                taskMarkerPosition.y = ((tasks[i].transform.localPosition.x - scenarioInferiorX) / (scenarioSupperiorX - scenarioInferiorX)) * (imgSupperiorY - imgInferiorY) + imgInferiorY;
                taskMarkers[i].GetComponent<RectTransform>().anchoredPosition = taskMarkerPosition;
            }*/
            
        }

    }

}
