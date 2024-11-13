using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChangeCamera : MonoBehaviour
{
    public GameObject[] cameraList;
    [FormerlySerializedAs("playerMode")] public GameObject player;
    public GameObject dronMode;

    private void EnablePlayerRoomMode(bool state)
    { // La camara 0 es la principal de Room
        cameraList[0].gameObject.SetActive(true);
        player.gameObject.SetActive(state);
    }
    private void EnableDronMode(bool state)
    { // La camara 1 es la principal del modo Dron
        cameraList[1].gameObject.SetActive(true);
        dronMode.gameObject.SetActive(state);
    }

    private void EnableAllExteriorCameras(bool state)
    {
        cameraList[2].gameObject.SetActive(state);
        cameraList[3].gameObject.SetActive(state);
        cameraList[4].gameObject.SetActive(state);
    }
    
    void Start()
    {
        EnablePlayerRoomMode(true);
        EnableDronMode(false);
    }

    void Update()
    {
        // Jugador pasa a modo de juego Room y se deshabilita el modo Dron.
        if (Input.GetKey(KeyCode.Alpha1))
        {
            EnablePlayerRoomMode(true);
            EnableDronMode(false);
        }
        
        // Jugador pasa al modo Dron y se deshabilita el modo Room
        if (Input.GetKey(KeyCode.Alpha2))
        {
            EnablePlayerRoomMode(false);
            EnableDronMode(true);
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
