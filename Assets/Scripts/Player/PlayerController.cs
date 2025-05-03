using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Room
{
    public class PlayerController : MonoBehaviour
    {
        public float verticalInput;
        public float horizontalInput;
        public float speed;
        public float rotationSpeed;
        
        public bool movementAudioOn = false;
        public AudioSource movementAudio;
        
        void OnEnable()
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
    
        // Update is called once per frame
        void Update()
        {
            // Controles de movimiento:
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
            transform.position += movement * speed * Time.deltaTime;
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(movement),rotationSpeed * Time.deltaTime);
            }
            
            // Animacion:
            float curSpeed = speed * movement.magnitude;
            GetComponent<Animator>().SetFloat("Speed", curSpeed);
            
            // Sonido
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
        }
    }
}

