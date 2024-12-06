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
    public Collider target;
    
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
        if (Input.GetKeyDown(KeyCode.Space) && target.gameObject.tag == "Player" && isOnSensor)
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
            if (this.gameObject.name == "Console Sensor")
            {
                if (!levelManager.consoleOn)
                {
                    levelManager.PlayerChangeToConsoleMode(true);
                }
                else
                {
                    levelManager.PlayerChangeToConsoleMode(false);
                }
            }
        }

        // Mientras se mantenga la tecla pulsada:
        if (Input.GetKey(KeyCode.Space) && target.gameObject.tag == "Player" && isOnSensor)
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
        
        // Tareas del exterior: PENDIENTE
        if (this.gameObject.name == "Task1 Sensor" && target.gameObject.tag == "Dron" && isOnSensor)
        {
            levelManager.TaskInProgress(true, 1);
            this.gameObject.SetActive(false);
        } 
        else if (this.gameObject.name == "Task2 Sensor" && target.gameObject.tag == "Dron" && isOnSensor)
        {
            levelManager.TaskInProgress(true, 2);
            this.gameObject.SetActive(false);
        } 
        else if (this.gameObject.name == "Task3 Sensor" && target.gameObject.tag == "Dron" && isOnSensor)
        {
            levelManager.TaskInProgress(true, 3);
            this.gameObject.SetActive(false);
        }
        
        // Final victoria de la partida.
        if (this.gameObject.name == "Door Exit Sensor" && target.gameObject.tag == "Player" && isOnSensor)
        {
            levelManager.GameWin();
        }

    }
}


