using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorDetectPresence : MonoBehaviour
{
    private TromerLevelManager levelManager;
    private void OnEnable()
    {
        levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
    }

    public bool player = false;
    public bool dron = false;
    
    private void OnTriggerStay(Collider other)
    {
        /*if (levelManager.consoleOn == false)
        {
            if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Space))
            {
                levelManager.PlayerControlConsoleMode(true);
            }
        }*/
        if (other.gameObject.tag == "Player")
        {
            player = true;
        }

        if (other.gameObject.tag == "Dron")
        {
            dron = true;
        }
        
    }
        
    private void OnTriggerExit(Collider other)
    {
       
            
    }
}


