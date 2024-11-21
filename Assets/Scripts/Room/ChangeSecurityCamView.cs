using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Room
{
    public class ChangeSecurityCamView : MonoBehaviour
    {
        public List<GameObject> cameras;
        public GameObject cameraSelected;
        
        private void OnMouseDown()
        {
            cameraSelected = this.gameObject;
            SwitchCamera();
        }
        
        private void SwitchCamera()
        {
            switch (cameraSelected.name)
            {
                case "Button Camera 1":
                    cameras[0].gameObject.SetActive(true);
                    TurnOtherCamerasOff(cameras[0]);
                    break;
                case "Button Camera 2":
                    cameras[1].gameObject.SetActive(true);
                    TurnOtherCamerasOff(cameras[1]);
                    break;
                case "Button Camera 3":
                    cameras[2].gameObject.SetActive(true);
                    TurnOtherCamerasOff(cameras[2]);
                    break;
            }
        }

        private void TurnOtherCamerasOff(GameObject cameraActive)
        {
            for (int i = 0; i < cameras.Count; i++)
            {
                if (cameras[i] != cameraActive)
                {
                    cameras[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

