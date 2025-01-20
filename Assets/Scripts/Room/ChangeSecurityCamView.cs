using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Room
{
    public class ChangeSecurityCamView : MonoBehaviour
    {
        private TromerLevelManager levelManager;
        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
        }
        
        public GameObject buttonCameraSelected;
        

        private void Update()
        {
            // Desactivar las camaras innecesarias al inicio de la partida.
            TurnOtherCamerasOff(levelManager.cameraSelected);
        }
        
        public void SwitchCamera()
        {
            buttonCameraSelected = this.gameObject;
            for (int i = 0; i < levelManager.securityCamerasButtons.Count; i++)
            {
                if (buttonCameraSelected.name == levelManager.securityCamerasButtons[i].name)
                {
                    levelManager.cameraSelected = i;
                    levelManager.securityCameras[i].gameObject.SetActive(true);
                    levelManager.securityCamerasScreens[i].gameObject.SetActive(true);
                    TurnOtherCamerasOff(levelManager.cameraSelected);
                }
            }

            buttonCameraSelected = null;
        }

        private void TurnOtherCamerasOff(int cameraActive)
        {
            for (int i = 0; i < levelManager.securityCamerasScreens.Count; i++)
            {
                if (i != cameraActive)
                {
                    //levelManager.securityCameras[i].gameObject.SetActive(false);
                    levelManager.securityCamerasScreens[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

