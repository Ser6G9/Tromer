using System;
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

    public bool isOnSensor = false;
    
    private void OnTriggerEnter(Collider other)
    {
        isOnSensor = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isOnSensor = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.gameObject.name == "Terminal Sensor")
            {
                if (isOnSensor && !levelManager.terminalOn)
                {
                    levelManager.PlayerChangeToTerminalMode(true);
                }
                else if (isOnSensor && levelManager.terminalOn)
                {
                    levelManager.PlayerChangeToTerminalMode(false);
                }
            }
        }
    }
}


