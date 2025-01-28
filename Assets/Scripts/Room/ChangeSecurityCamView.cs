using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
            for (int i = 0; i < levelManager.securityCamerasScreens.Count; i++)
            {
                if (buttonCameraSelected.name == levelManager.securityCamerasScreens[i].name)
                {
                    levelManager.cameraSelected = i;
                    levelManager.securityCameras[i].gameObject.SetActive(true);
                    levelManager.securityCamerasScreens[i].gameObject.SetActive(true);
                    levelManager.securityCamerasButtons[i].GetComponent<Image>().color = new Color(0.66f, 1f, 0.18f, 1f);
                    
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
                    levelManager.securityCamerasButtons[i].GetComponent<Image>().color = new Color(0.92f, 0.92f, 0.92f, 1.0f);
                }
            }
        }
        //levelManager.securityCamerasButtons[i].GetComponent<Image>().color = new Color(1f, 0.3559244f, 0.2509804f, 1f);
    }
}

