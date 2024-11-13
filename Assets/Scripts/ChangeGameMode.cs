using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChangeGameMode : MonoBehaviour
{
    public GameObject roomModeCamera;
    public GameObject roomModeControls;

    public GameObject dronModeCamera;
    public GameObject dronModeControls;

    public GameObject[] exteriorCameraList;

    private void RoomMode(bool state)
    {
        roomModeCamera.gameObject.SetActive(state);
        roomModeControls.gameObject.SetActive(state);
    }
    private void DronMode(bool state)
    {
        dronModeCamera.gameObject.SetActive(state);
        dronModeControls.gameObject.SetActive(state);
    }

    /*private void EnableAllExteriorCameras(bool state)
    {
        exteriorCameraList[0].gameObject.SetActive(state);
        exteriorCameraList[1].gameObject.SetActive(state);
        exteriorCameraList[2].gameObject.SetActive(state);
    }*/
    
    void Start()
    {
        RoomMode(true);
        DronMode(false);
    }

    void Update()
    {
        // Jugador pasa a modo de juego Room y se deshabilita el modo Dron.
        if (Input.GetKey(KeyCode.Alpha1))
        {
            RoomMode(true);
            DronMode(false);
        }
        
        // Jugador pasa al modo Dron y se deshabilita el modo Room
        if (Input.GetKey(KeyCode.Alpha2))
        {
            RoomMode(false);
            DronMode(true);
        }
    }
    
    /*private void OnTriggerEnter(Collider other) // TODO: INTENTO de el sensor
    {
        if (other.gameObject.tag == "Player") // "Player" es un Tag que se le ha asignado al player desde el inspector.
        {
            cameraList[0].gameObject.SetActive(false);
            cameraList[1].gameObject.SetActive(true);
            
            room.gameObject.SetActive(false);
            exterior.gameObject.SetActive(true);
        }
    }*/
}
