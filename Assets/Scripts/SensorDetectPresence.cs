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
        // Si solo se pulsa una sola vez:
        if (Input.GetKeyDown(KeyCode.Space) && isOnSensor)
        {
            if (this.gameObject.name == "Terminal Sensor")
            {
                if (!levelManager.terminalOn)
                {
                    levelManager.PlayerChangeToTerminalMode(true);
                }
                else
                {
                    levelManager.PlayerChangeToTerminalMode(false);
                }
            }
        }

        // Mientras se mantenga la tecla pulsada:
        if (Input.GetKey(KeyCode.Space) && isOnSensor)
        {
            if (this.gameObject.name == "Oxigen Sensor")
            {
                levelManager.oxigenIncrementationOn = true;
            }
        }
        else
        {
            levelManager.oxigenIncrementationOn = false;
        }
        
        // Final de la partida.
        if (this.gameObject.name == "Door Exit Sensor" && isOnSensor)
        {
            levelManager.GameWin();
        }

    }
}


