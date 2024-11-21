using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Room
{
    public class ChangeSecurityCamView : MonoBehaviour
    {
        private TromerLevelManager levelManager;
        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
        }
        
        public GameObject cameraSelected;

        private void Start()
        {
            // Desactivar las camaras innecesarias al inicio de la partida.
            TurnOtherCamerasOff(0);
        }

        private void OnMouseDown()
        {
            cameraSelected = this.gameObject;
            SwitchCamera();
        }
        
        private void SwitchCamera()
        {
            for (int i = 0; i < levelManager.securityCamerasButtons.Count; i++)
            {
                if (cameraSelected.name == levelManager.securityCamerasButtons[i].name)
                {
                    levelManager.securityCameras[i].gameObject.SetActive(true);
                    levelManager.securityCamerasScreens[i].gameObject.SetActive(true);
                    TurnOtherCamerasOff(i);
                }
            }
        }

        private void TurnOtherCamerasOff(int cameraActive)
        {
            for (int i = 0; i < levelManager.securityCamerasScreens.Count; i++)
            {
                if (i != cameraActive)
                {
                    levelManager.securityCameras[i].gameObject.SetActive(false);
                    levelManager.securityCamerasScreens[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

