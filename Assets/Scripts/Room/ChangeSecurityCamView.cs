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

        private void Start()
        {
            // Desactivar las camaras innecesarias al inicio de la partida.
            TurnOtherCamerasOff(0);
        }
        
        public void SwitchCamera()
        {
            buttonCameraSelected = this.gameObject;
            for (int i = 0; i < levelManager.securityCamerasButtons.Count; i++)
            {
                if (buttonCameraSelected.name == levelManager.securityCamerasButtons[i].name)
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
                    //levelManager.securityCameras[i].gameObject.SetActive(false);
                    levelManager.securityCamerasScreens[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

