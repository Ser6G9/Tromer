using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SensorDetectPresence : MonoBehaviour
{
    private TromerLevelManager levelManager;
    private void OnEnable()
    {
        levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
    }

    public bool isOnSensor = false;
    public Collider target;
    public GameObject actionObject;
    public bool isInteractionText = false;
    
    private void OnTriggerEnter(Collider other)
    {
        target = other;
        isOnSensor = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isOnSensor = false;
    }

    private void Update()
    {
        // Mostrar textos flotantes de los objetos en la Room:
        if (isInteractionText == true)
        {
            if (isOnSensor && actionObject != null)
            {
                actionObject.SetActive(true);
            }
            else
            { 
                actionObject.SetActive(false);
            }
        }
        

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

            if (this.gameObject.name == "OptiTask1 Sensor")
            {
                if (!levelManager.optiTask1On)
                {
                    levelManager.PlayerChangeToOptiTaskMode(true, 1);
                }
                else
                {
                    levelManager.PlayerChangeToOptiTaskMode(false, 1);
                }
            }
        }

        // Final victoria de la partida.
        if (this.gameObject.name == "Door Exit Sensor" && target.gameObject.tag == "Player" && isOnSensor)
        {
            levelManager.GameWin();
        }
        
        
        // Mientras se mantenga la tecla pulsada:
        if (Input.GetKey(KeyCode.Space) && target.gameObject.tag == "Player" && isOnSensor)
        {
            if (this.gameObject.name == "Oxigen Sensor")
            {
                levelManager.oxigenIncrementationOn = true;
            }
        }
        else if (this.gameObject.name == "Oxigen Sensor")
        {
            levelManager.oxigenIncrementationOn = false;
        }
            
        
        
        
        // Tareas del exterior: PENDIENTE
        if (target.gameObject.tag == "Dron" && isOnSensor)
        {
            if (this.gameObject.name == "Task1 Sensor")
            {
                levelManager.TaskInProgress(true, 1);
                actionObject.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
            else if (this.gameObject.name == "Task2 Sensor")
            {
                levelManager.TaskInProgress(true, 2);
                actionObject.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
            else if (this.gameObject.name == "Task3 Sensor")
            {
                levelManager.TaskInProgress(true, 3);
                actionObject.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }

    }
}


