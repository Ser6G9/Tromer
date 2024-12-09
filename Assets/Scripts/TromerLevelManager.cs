using System;
using System.Collections;
using System.Collections.Generic;
using Exterior;
using Room;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TromerLevelManager : MonoBehaviour
{
    // Elementos del HUD:
    public GameObject youWin;
    public GameObject youLose;
    public GameObject uControls;
    public GameObject uControlsGuide;
    public bool uControlsGuideOn = false;
    public GameObject pPause;
    public GameObject pauseMenu;
    public bool pauseMenuOn = false;

    // Cada vez que el dron recoja una coin, el HUD sumará +1 al contador
    public TextMeshProUGUI coinsText;
    public int coins = 0;
    
    // Para cambiar de modos/pantallas de juego:
    public GameObject roomPlayerCamera;
    public GameObject roomPlayer;
    public GameObject dron;
    // Terminal
    public bool terminalOn = false;
    public GameObject terminalCamera;
    public GameObject terminalText;
    // Consola
    public bool consoleOn = false;
    public GameObject consoleCamera;
    public GameObject consoleText;
    // Tareas Opcionales
    public bool optiTask1On = false;
    public GameObject optiTask1Camera;
    public GameObject optiTask1Text;

    // Controles de las camaras de seguridad:
    public List<GameObject> securityCameras;
    public List<GameObject> securityCamerasScreens;
    public List<GameObject> securityCamerasButtons;
    
    // Tareas del exterior: 0 = Task1, 1 = Task2, 2 = Task3
    public int tasksCompleteCount = 0;
    public List<bool> tasksStates;

    public bool openExitDoor = false;
    public GameObject roomExitDoor;
    
    // Gestión del contador de Oxígeno:
    public float totalOxigenTime = 60;
    public float oxigenProgressTime;
    public bool oxigenIncrementationOn = false;
    public float oxigenIncrementationSpeed = 0.4f;
    public TextMeshProUGUI oxigenProgressText;
    public Slider oxigenSliderProgress;
    public GameObject oxigenLevel3DProgress;
    public GameObject oxigen3DText;
    public float oxigenPercentage = 100;
    
    private void Start()
    {
        Time.timeScale = 1f;
        youWin.SetActive(false);
        youLose.SetActive(false);
        
        oxigenProgressTime = totalOxigenTime;
        ShowOxigenLevelProgress();
        
        PlayerChangeToTerminalMode(false);
        PlayerChangeToConsoleMode(false);
    }

    private void Update()
    {
        // Se pausa el juego
        if (youWin.activeSelf || youLose.activeSelf || pauseMenuOn)
        {
            Time.timeScale = 0f;
        } else if (!pauseMenuOn)
        {
            Time.timeScale = 1f;
        }
        
        ShowOxigenLevelProgress();
        if (oxigenIncrementationOn)
        {
            OxigenIncrementation();
        }
        else
        {
            OxigenCountDawnProgress();
        }
        
        UpdateCoinsText();

        if (openExitDoor == true)
        {
            OpenRoomExitDoor();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            uControlsGuideOn = !uControlsGuideOn;
        }
        if (uControlsGuideOn)
        {
            uControls.SetActive(false);
            uControlsGuide.SetActive(true);
        } 
        else
        {
            uControls.SetActive(true);
            uControlsGuide.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenuOn = !pauseMenuOn;
        }
        PauseMenuInteraction(pauseMenuOn);
    }

    public void PauseMenuInteraction(bool state)
    {
        pauseMenuOn = state;
        if (pauseMenuOn)
        {
            pPause.SetActive(false);
            pauseMenu.SetActive(true);
        } 
        else
        {
            pPause.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }
    
    /* -- ROOM -- */
    
    // Cambiar cámara y controles del player al modo Terminal:
    public void PlayerChangeToTerminalMode(bool state)
    {
        terminalCamera.gameObject.SetActive(state);
        dron.GetComponent<DronController>().enabled = state;
        
        ChangeRoomPlayerState(state);
        
        terminalOn = state;
    }

    // Cambiar cámara al modo Consola:
    public void PlayerChangeToConsoleMode(bool state)
    {
        consoleCamera.gameObject.SetActive(state);
        
        ChangeRoomPlayerState(state);

        consoleOn = state;
    }

    // Cuenta atrás del contador oxígeno (Objeto 3d y barra del HUD):
    public void OxigenCountDawnProgress()
    {
        if (oxigenProgressTime >= 0.0f) 
        {
            oxigenProgressTime -= Time.deltaTime;
        }
        else
        {
            // Si el tiempo ha llegado a 0 entonces: Game Over
            GameOver();
        }
    }

    // Incrementar el medidor de oxígeno:
    public void OxigenIncrementation()
    {
        if (oxigenProgressTime <= totalOxigenTime && oxigenProgressTime > 0.0f) 
        {
            // El oxigeno se incrementará en relación a la velocidad de incremntación
            oxigenProgressTime += Time.deltaTime * oxigenIncrementationSpeed;
        }
    }

    public void ShowOxigenLevelProgress()
    {
        if (oxigenProgressTime >= 0.0f)
        {
            oxigenPercentage = (oxigenProgressTime / totalOxigenTime) * 100;
            oxigenProgressText.text = $"{oxigenPercentage:F0}%";
            
            // Barra de oxígeno del HUD:
            oxigenSliderProgress.value = oxigenPercentage / 100.0f;
            
            // Altura del modelo 3D del contador
            float currentHeight = Mathf.Lerp(-2, 2, oxigenPercentage / 100.0f);
            Vector3 position = oxigenLevel3DProgress.transform.position;
            position.y = currentHeight;
            oxigenLevel3DProgress.transform.position = position;
        }
    }
    
    // Cambiar cámara al modo Consola:
    public void PlayerChangeToOptiTaskMode(bool state, int num)
    {
        switch (num)
        {
            case 1:
                optiTask1Camera.gameObject.SetActive(state);
                break;
        }
        
        ChangeRoomPlayerState(state);

        optiTask1On = state;
    }

    // Abrir la puerta de salida:
    public void OpenRoomExitDoor()
    {
        Vector3 doorOpenPosition = new Vector3(0.0f, 4.6f, 11.31371f);
        roomExitDoor.transform.position = Vector3.MoveTowards(roomExitDoor.transform.position, doorOpenPosition, 0.2f * Time.deltaTime);
        
        if (Vector3.Distance(roomExitDoor.transform.position, doorOpenPosition) < 0.01f)
        {
            transform.position = doorOpenPosition;
            openExitDoor = false;
        }
        // habilitar el sensor de youWin
    }

    // Alternar el modo de juego de la Room:
    public void ChangeRoomPlayerState(bool state)
    {
        roomPlayerCamera.gameObject.SetActive(!state);
        roomPlayer.gameObject.SetActive(!state);
        for (int i = 0; i < roomPlayer.transform.childCount; i++) // También se desactivan/activan los componentes del GameObject Player
        {
            roomPlayer.transform.GetChild(i).gameObject.SetActive(!state);
        }
    }

    public void GameOver()
    {
        youLose.SetActive(true);
    }
    public void GameWin()
    {
        youWin.SetActive(true);
    }
    
    
    /* -- EXTERIOR -- */
    
    public void UpdateCoinsText()
    {
        coinsText.text = coins.ToString();
    }

    // Progreso de las tareas del exterior: PENDIENTE se completan al instate
    public void TaskInProgress(bool state, int taskID)
    {
        tasksStates[taskID-1] = state;
        for (int i = 0; i < tasksStates.Count; i++)
        {
            if (tasksStates[i] == true)
            {
                tasksCompleteCount++;
            }
        }

        if (tasksCompleteCount == tasksStates.Count)
        {
            openExitDoor = true;
        }

        tasksCompleteCount = 0;
    }
}
