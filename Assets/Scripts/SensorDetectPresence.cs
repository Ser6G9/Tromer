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
    
    private void OnTriggerStay(Collider other)
    { 
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Space))
        {
            levelManager.PlayerControlTerminalMode(true);
        }
    }
        
    private void OnTriggerExit(Collider other)
    {
       
            
    }
}


