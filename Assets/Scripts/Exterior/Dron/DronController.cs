using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

namespace Exterior
{
    public class DronController : MonoBehaviour
    {
        public float verticalInput;
        public float horizontalInput;
        public float speed;
        public float rotationSpeed;
        
        public bool movementAudioOn = false;
        public AudioSource movementAudio;
        
    
        // Update is called once per frame
        void Update()
        {
            // Controles de movimiento:
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            
            transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
            transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);

            if ((horizontalInput != 0 || verticalInput != 0) && !movementAudioOn)
            {
                movementAudio.Play();
                movementAudioOn = true;
            }
            else if(horizontalInput == 0 && verticalInput == 0)
            {
                movementAudioOn = false;
                movementAudio.Stop();
            }
            
            // Detectar el movimiento del raton:
            /*float mouseX = Input.GetAxis("Mouse X");
            Debug.Log(mouseX);*/
        }

        private void OnDisable()
        {
            movementAudioOn = false;
            movementAudio.Stop();
        }
    }
}

