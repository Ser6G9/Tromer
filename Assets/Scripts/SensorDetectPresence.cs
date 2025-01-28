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
    public bool haveInteractionText = false;
    
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
        if (haveInteractionText == true)
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
            
            if (this.gameObject.name == "Oxigen Emergency Sensor")
            {
                levelManager.emergencyRepairProgressOn = true;
            }
            
        }
        else if (this.gameObject.name == "Oxigen Sensor")
        {
            levelManager.oxigenIncrementationOn = false;
        }
        else if (this.gameObject.name == "Oxigen Emergency Sensor")
        {
            levelManager.emergencyRepairProgressOn = false;
        }
            
        
        
        
        // Tareas del exterior:
        if (target.gameObject.tag == "Dron" && isOnSensor)
        {
            if (this.gameObject.name == "Task1 Sensor")
            {
                // Si la tarea aún no está completada, se aumenta su progreso.
                if (levelManager.GetTaskState(1) == false)
                {
                    levelManager.TaskInProgress(1);
                }
                else
                {
                    // Si ya ha sido completada, se desactiva la luz y el sensor de la tarea.
                    actionObject.gameObject.SetActive(false); 
                    this.gameObject.SetActive(false);
                }
            }
            else if (this.gameObject.name == "Task2 Sensor")
            {
                // Si la tarea aún no está completada, se aumenta su progreso.
                if (levelManager.GetTaskState(2) == false)
                {
                    levelManager.TaskInProgress(2);
                }
                else
                {
                    // Si ya ha sido completada, se desactiva la luz y el sensor de la tarea.
                    actionObject.gameObject.SetActive(false); 
                    this.gameObject.SetActive(false);
                }
            }
            else if (this.gameObject.name == "Task3 Sensor")
            {
                // Si la tarea aún no está completada, se aumenta su progreso.
                if (levelManager.GetTaskState(3) == false)
                {
                    levelManager.TaskInProgress(3);
                }
                else
                {
                    // Si ya ha sido completada, se desactiva la luz y el sensor de la tarea.
                    actionObject.gameObject.SetActive(false); 
                    this.gameObject.SetActive(false);
                }
            }
        }

    }
}


