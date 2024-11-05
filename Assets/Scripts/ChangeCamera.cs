using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject[] cameraList;
    public GameObject room;
    public GameObject exterior;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraList[0].gameObject.SetActive(true);
        cameraList[1].gameObject.SetActive(false);
        room.gameObject.SetActive(true);
        exterior.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Jugador pasa a Room y se deshabilita Exterior
        if (Input.GetKey(KeyCode.Alpha1))
        {
            cameraList[0].gameObject.SetActive(true);
            cameraList[1].gameObject.SetActive(false);
            
            room.gameObject.SetActive(true);
            exterior.gameObject.SetActive(false);
        }
        
        // Jugador pasa a Exterior y se deshabilita Room
        if (Input.GetKey(KeyCode.Alpha2))
        {
            cameraList[0].gameObject.SetActive(false);
            cameraList[1].gameObject.SetActive(true);
            
            room.gameObject.SetActive(false);
            exterior.gameObject.SetActive(true);
        }
    }
}
