using System;
using System.Collections;
using System.Collections.Generic;
using Exterior;
using Room;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    private ScoreManager scoreManager;
    private EnemyEventsManager enemyEventsManager;
    private void OnEnable()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        enemyEventsManager = GameObject.FindObjectOfType<EnemyEventsManager>();
    }
    
    // Elementos del HUD:
    public GameObject youWin;
    public AudioSource youWinMussic;
    public GameObject youLose;
    public AudioSource youLooseSound;
    public GameObject uTutorial;
    public List<GameObject> tutorialPanels;
    public bool uTutorialOn = false;
    public GameObject pPause;
    public GameObject pauseMenu;
    public TextMeshProUGUI pauseMenuScore;
    public bool pauseMenuOn = false;
    public TextMeshProUGUI currentPlayTime;
    public GameObject alertDoorOpenTxt;
    public GameObject exitInteractionTxt;
    public GameObject saveScoreMenu;
    public bool saveScoreMenuOn = false;

    // Cada vez que el dron recoja una coin, el HUD sumará +1 al contador
    public TextMeshProUGUI coinsText;
    public int coins = 0;
    
    // Puntuación
    public int score;
    public bool scoreSet= false;
    
    // PLAYER:
    public GameObject roomPlayerCamera;
    public GameObject roomPlayer;
    // ENEMY:
    public GameObject enemy;
    public GameObject enemyInstance;
    // DRON:
    public GameObject dron;
    public ParticleSystem dronDisabledParticles;
    public AudioSource dronDisabledSoundEffect;
    public bool dronEnabled = true;
    public float dronMaxTimeToBeEnabled = 15.0f;
    public float dronEnableTimer = 0.0f;
    public float dronReparingPercentage = 0.0f;
    
    // Terminal
    public bool terminalOn = false;
    public int cameraSelected = -1;
    public GameObject terminalCamera;
    public GameObject terminalCanvas;
    // Consola
    public bool consoleOn = false;
    public GameObject consoleCamera;
    public GameObject consoleCanvas;
    
    // Tareas Opcionales
    public int optiTasksCompleteCount = 0;
    // +50% de Oxígeno extra:
    public bool optiTask1On = false;
    public GameObject optiTask1Camera;
    public GameObject optiTask1HUDText;
    public GameObject optiTask1Canvas;
    // Reparar el Dron:
    public bool optiTask2On = false;
    public GameObject optiTask2Camera;
    public GameObject optiTask2HUDText;
    public TextMeshProUGUI optiTask2HUDProgressText;

    // Controles de las camaras de seguridad:
    public List<GameObject> securityCameras;
    public List<GameObject> securityCamerasScreens;
    public List<Button> securityCamerasButtons;
    
    // Controles de las puertas de seguridad:
    public List<GameObject> securityDoors;
    public List<Button> securityDoorsButtons;
    
    // Lista de tareas del exterior a completar: 0 = Task1, 1 = Task2, 2 = Task3
    public float timeToCompleteTasks = 30;
    public int tasksCompleteCount = 0;
    public List<bool> tasksExteriorState;
    public List<Slider> tasksHUDSlider;
    public List<TextMeshProUGUI> tasksHUDProgressText;
    public List<Image> tasksHUDProgressBar;
    public List<TextMeshProUGUI> tasksObjectiveText;
    public List<float> tasksCurrentProgressTime;
    public float taskProgressPercentage;
    public AudioSource taskCompleteSound;
    
    // Eventos de emergencia:
    public float timeToRepairEmergency = 2f;
    public bool emergencyActive = false;
    public float countDawnToAppearNextEmergency;
    public float timeMinToAppearNextEmergency = 20f;
    public float timeMaxToAppearNextEmergency = 40f;
    public bool emergencyRepairInProgressOn = false;
    public float repairEmergencyProgress = 0;
    public GameObject emergencyHUDText;
    public Slider emergencyHUDSlider;
    public Slider emergencyHUDSlider3D;
    public TextMeshProUGUI emergencyHUDPercentage;
    public bool emergencySoundEffectOn = false;
    public AudioSource emergencySoundEffect;
    // - Se reduce el oxigeno más rápido
    public GameObject emergencyOxigen;
    public float emergencyOxigenReductionSpeedMultiplier = 8f;

    // La puerta de salida (victoria):
    public bool openExitDoor = false;
    public bool openExitDoorSoundEffectOn = false;
    public AudioSource roomExitDoorSoundEffect;
    public GameObject roomExitDoor;
    public GameObject lightRoomExitDoor;
    
    // Gestión del contador de Oxígeno:
    public float totalOxigenTime = 220;
    public float oxigenProgressTime = 0;
    public bool oxigenIncrementationOn = false;
    public float oxigenIncrementationSpeed = 0.5f;
    public TextMeshProUGUI oxigenProgressText;
    public Slider oxigenSliderProgress;
    public GameObject oxigenLevelColor;
    public GameObject incrementationMark;
    public GameObject oxigenLevel3DProgress;
    public GameObject oxigenLevel3DExteriorProgress;
    public float oxigenPercentage = 100;
    public Material oxigenNormalMaterial;
    public Material oxigenEmergencyMaterial;
    
    
    private void Start()
    {
        Time.timeScale = 1f;
        youWin.SetActive(false);
        youLose.SetActive(false);
        saveScoreMenu.SetActive(false);
        
        ShowTutorial(false);
        
        oxigenProgressTime = totalOxigenTime;
        ShowOxigenLevelProgress();
        
        PlayerChangeToTerminalMode(false);
        PlayerChangeToConsoleMode(false);
        PlayerChangeToOptiTaskMode(false, 1);

        DeactivateEmergency();
    }

    private void Update()
    {
        
        CheckIfGameIsPaused();
        
        // -- Cronometro del tiempo de juego --
        currentPlayTime.text = "Tiempo de juego: "+ scoreManager.totalPlayTime;
        
        // -- ¿Se está aumentando o no el Oxigeno?
        ShowOxigenLevelProgress();
        if (oxigenIncrementationOn)
        {
            OxigenIncrementation();
        }
        else
        {
            OxigenCountDawnProgress();
        }
        
        // Objetos recolectables
        /*UpdateCoinsText();*/

        // -- Emergencias:
        if (emergencyActive)
        {
            ShowEmergencyProgress();
        }
        
        // El jugador está reparando la emergencia.
        if (emergencyRepairInProgressOn)
        {
            ReparingEmergency();
        }
        // Si no hay una Emergencia en espera de aparecer (activa), se activa otra nueva y se le asigna un tiempo de espera de aparición.
        else if (emergencyActive == false)
        {
            countDawnToAppearNextEmergency = Random.Range(timeMinToAppearNextEmergency, timeMaxToAppearNextEmergency);
            emergencyActive = true;
        }
        // Si su tiempo de aparición llega a 0 se creará una Emergencia en la partida.
        else if (countDawnToAppearNextEmergency >= 0.0f)
        {
            countDawnToAppearNextEmergency -= Time.deltaTime;
        }
        else
        {
            CreateEmergency();
        }
        
        // Autorreparación del dron con el tiempo
        // -- HUD:
        optiTask2HUDText.gameObject.SetActive(!dronEnabled);
        if (!dronEnabled && dronEnableTimer <= dronMaxTimeToBeEnabled)
        {
            dronEnableTimer += Time.deltaTime;
            dronReparingPercentage = (dronEnableTimer / dronMaxTimeToBeEnabled) * 100;
            optiTask2HUDProgressText.text = "Reparando: "+$"{dronReparingPercentage:F0}%";
        } else if (dronEnableTimer > dronMaxTimeToBeEnabled)
        {
            dronEnableTimer = 0;
            DronEnabled(true);
        }

        // -- Abrir puerta de final de partida
        if (openExitDoor == true)
        {
            OpenRoomExitDoor();
        }

        // Se deshabilitan las interacciones con el HUD del juego según la siuación.
        if (!saveScoreMenuOn && !youLose.activeSelf && !youWin.activeSelf)
        {
            // -- Pausar juego
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenuOn = !pauseMenuOn;
                if (uTutorialOn)
                {
                    ShowTutorial(false);
                }
            }
            PauseMenuInteraction(pauseMenuOn);
        }

        
    }

    public bool CheckIfGameIsPaused()
    {
        // -- Se pausa el juego si ganas/pierdes o pones en pausa
        if (youWin.activeSelf || youLose.activeSelf || pauseMenuOn || uTutorialOn)
        {
            Time.timeScale = 0f;
            return true;
        } else if (!pauseMenuOn && !uTutorialOn)
        {
            Time.timeScale = 1f;
            return false;
        }
        return false;
    }

    public void ShowTutorial(bool state)
    {
        uTutorialOn = state;
        uTutorial.SetActive(state);

        if (uTutorialOn)
        {
            Time.timeScale = 0f;
            NextTutorialPanel(0);
        } else {
            Time.timeScale = 1f;
        }
    }
    public void NextTutorialPanel(int index)
    {
        for (int i = 0; i < tutorialPanels.Count; i++)
        {
            if (i == index)
            {
                tutorialPanels[i].SetActive(true);
            }
            else
            {
                tutorialPanels[i].SetActive(false);
            }
        }
    }

    public void PauseMenuInteraction(bool state)
    {
        pauseMenuOn = state;
        if (pauseMenuOn)
        {
            pPause.SetActive(false);
            pauseMenu.SetActive(true);
            pauseMenuScore.text = "Puntuación actual:\n"+scoreManager.totalScore;
            emergencySoundEffect.Pause();
            dronDisabledSoundEffect.Pause();
            roomExitDoorSoundEffect.Pause();
            roomPlayer.GetComponent<PlayerController>().movementAudio.Stop();
        } 
        else
        {
            pPause.SetActive(true);
            pauseMenu.SetActive(false);
            emergencySoundEffect.UnPause();
            dronDisabledSoundEffect.UnPause();
            roomExitDoorSoundEffect.UnPause();
        }
    }
    
    public void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
    }
    
    /* -- ROOM -- */
    
    // Cambiar cámara y controles del player al modo Terminal:
    public void PlayerChangeToTerminalMode(bool state)
    {
        exitInteractionTxt.gameObject.SetActive(state);
        terminalCamera.gameObject.SetActive(state);
        terminalCanvas.GetComponent<GraphicRaycaster>().enabled = state;
        if(dronEnabled)
        {
            dron.GetComponent<DronController>().enabled = state;
        }
        
        ChangeRoomPlayerState(state);
        
        terminalOn = state;
    }

    // Cambiar cámara al modo Consola:
    public void PlayerChangeToConsoleMode(bool state)
    {
        exitInteractionTxt.gameObject.SetActive(state);
        consoleCamera.gameObject.SetActive(state);
        consoleCanvas.GetComponent<GraphicRaycaster>().enabled = state;
        
        ChangeRoomPlayerState(state);

        consoleOn = state;
    }

    // Cuenta atrás del contador oxígeno (Objeto 3d y barra del HUD):
    public void OxigenCountDawnProgress()
    {
        incrementationMark.gameObject.SetActive(false);
        if (oxigenProgressTime >= 0.0f) 
        {
            // La Emergencia de oxígeno hace que se reduzca más rápidamente.
            if (emergencyOxigen.activeSelf)
            {
                oxigenProgressTime -= Time.deltaTime * emergencyOxigenReductionSpeedMultiplier;
                oxigenLevelColor.GetComponent<Image>().color = new Color(1f, 0.05088251f, 0f);
            }
            else
            {
                oxigenProgressTime -= Time.deltaTime;
                oxigenLevelColor.GetComponent<Image>().color = new Color(1f, 0.3882353f, 0.09803922f);
            }
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
        if (oxigenProgressTime <= totalOxigenTime && oxigenProgressTime > 0.0f) // && CheckIfGameIsPaused() == false
        {
            /*// El oxigeno se incrementará en relación a la velocidad de incremntación
            oxigenProgressTime += Time.deltaTime * oxigenIncrementationSpeed;*/
            
            incrementationMark.gameObject.SetActive(true);
            
            oxigenProgressTime += totalOxigenTime * 0.012f; // se sube un 1,2%
            scoreManager.totalOxigenIncrementations++;
            
            if (oxigenProgressTime >= totalOxigenTime)
            {
                oxigenProgressTime = totalOxigenTime;
            }
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
    
    // Cambiar cámara para las OptiTasks (Tareas Opcionales):
    public void PlayerChangeToOptiTaskMode(bool state, int num)
    {
        exitInteractionTxt.gameObject.SetActive(state);
        switch (num)
        {
            case 1:
                optiTask1Camera.gameObject.SetActive(state);
                optiTask1Canvas.GetComponent<GraphicRaycaster>().enabled = state;
                break;
            case 2:
                optiTask2Camera.gameObject.SetActive(state);
                optiTask2HUDText.GetComponent<GraphicRaycaster>().enabled = state;
                break;
        }
        
        ChangeRoomPlayerState(state);
        optiTask1On = state;
        
    }
    
    // Se crea una Emergencia nueva dentro del juego:
    public void CreateEmergency()
    {
        emergencyOxigen.SetActive(true);
        emergencyHUDText.SetActive(true);
        oxigenLevel3DProgress.GetComponent<MeshRenderer>().material = oxigenEmergencyMaterial;
        if (!emergencySoundEffectOn)
        {
            emergencySoundEffect.Play();
            emergencySoundEffectOn = true;
        }
    }
    public void ReparingEmergency()
    {
        // Si el tiempo de reparación se completa, se repara la Emergencia (desactiva).
        if (repairEmergencyProgress <= timeToRepairEmergency)
        {
            repairEmergencyProgress += Time.deltaTime;
        }
        else
        {
            DeactivateEmergency();
            emergencyRepairInProgressOn = false;
            repairEmergencyProgress = 0;
            
            scoreManager.totalEmergencysFixed++;
        }
    }
    public void DeactivateEmergency()
    {
        emergencyActive = false;
        emergencyOxigen.SetActive(false);
        emergencyHUDText.SetActive(false);
        oxigenLevel3DProgress.GetComponent<MeshRenderer>().material = oxigenNormalMaterial;
        
        emergencySoundEffectOn = false;
        emergencySoundEffect.Stop();
    }

    public void ShowEmergencyProgress()
    {
        float percentageProgress = (repairEmergencyProgress / timeToRepairEmergency) * 100.0f;
        emergencyHUDPercentage.text = $"{percentageProgress:F0}%";
        emergencyHUDSlider.value = percentageProgress / 100.0f;
        emergencyHUDSlider3D.value = percentageProgress / 100.0f;
    }

    // Abrir la puerta de salida:
    public void OpenRoomExitDoor()
    {
        if (!openExitDoorSoundEffectOn)
        {
            youWinMussic.Play();
            
            roomExitDoorSoundEffect.Play();
            openExitDoorSoundEffectOn = true;
        }
        
        alertDoorOpenTxt.gameObject.SetActive(true);
        lightRoomExitDoor.SetActive(true);
        Vector3 doorOpenPosition = new Vector3(0.0f, 4.6f, 11.31371f);
        roomExitDoor.transform.position = Vector3.MoveTowards(roomExitDoor.transform.position, doorOpenPosition, 0.2f * Time.deltaTime);
        
        if (Vector3.Distance(roomExitDoor.transform.position, doorOpenPosition) < 0.01f)
        {
            transform.position = doorOpenPosition;
            openExitDoor = false;
            openExitDoorSoundEffectOn = false;
            roomExitDoorSoundEffect.Stop();
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
        youWinMussic.Stop();
        alertDoorOpenTxt.SetActive(false);
        
        youLose.SetActive(true);
        if(!scoreSet)
        {
            youLooseSound.Play();
            score = scoreManager.CalculateTotalScore(false);
            scoreSet = true;
        }
        
        ShowSaveScoreMenu();
        emergencySoundEffect.Stop();
        dronDisabledSoundEffect.Stop();
        roomExitDoorSoundEffect.Stop();
        roomPlayer.GetComponent<PlayerController>().movementAudio.Stop();
    }
    public void GameWin()
    {
        alertDoorOpenTxt.SetActive(false);
        youWin.SetActive(true);
        if(!scoreSet)
        {
            score = scoreManager.CalculateTotalScore(true);
            scoreSet = true;
        }
        ShowSaveScoreMenu();
        emergencySoundEffect.Stop();
        dronDisabledSoundEffect.Stop();
        roomExitDoorSoundEffect.Stop();
        roomPlayer.GetComponent<PlayerController>().movementAudio.Stop();
    }
    
    public void ShowSaveScoreMenu()
    {
        saveScoreMenu.SetActive(true);
        saveScoreMenuOn = true;
    }
    
    
    /* -- EXTERIOR -- */
    
    /*public void UpdateCoinsText()
    {
        coinsText.text = coins.ToString();
    }*/

    public bool GetTaskState(int taskID)
    {
        return tasksExteriorState[taskID-1];
    }
    
    // Progreso de las tareas del exterior:
    public void TaskInProgress(int taskID)
    {
        if (tasksCurrentProgressTime[taskID-1] <= timeToCompleteTasks) 
        {
            tasksCurrentProgressTime[taskID-1] += Time.deltaTime;
            // La tarea de reiniciar ventilación tiene una "animación".
            if (taskID == 3)
            {
                taskProgressPercentage = (tasksCurrentProgressTime[taskID-1] / timeToCompleteTasks) * 100;
                float currentHeight = Mathf.Lerp(-2, 2, taskProgressPercentage / 100.0f);
                Vector3 position2 = oxigenLevel3DExteriorProgress.transform.position;
                position2.y = currentHeight;
                oxigenLevel3DExteriorProgress.transform.position = position2;
            }
        }
        
        float percentageProgress = (tasksCurrentProgressTime[taskID-1] / timeToCompleteTasks) * 100.0f;
        tasksHUDProgressText[taskID-1].text = $"{percentageProgress:F0}%";
        tasksHUDSlider[taskID-1].value = percentageProgress / 100.0f;

        // Si el progreso llega a su máximo, la tarea se completa.
        if (tasksCurrentProgressTime[taskID-1] >= timeToCompleteTasks)
        {
            TaskComplete(taskID);
        }
    }
    
    // Al completar una tarea:
    public void TaskComplete(int taskID)
    {
        tasksObjectiveText[taskID-1].color = Color.green;
        tasksHUDProgressText[taskID-1].color = Color.green;
        tasksHUDProgressBar[taskID-1].color = Color.green;
        tasksExteriorState[taskID-1] = true;
        tasksCompleteCount++;
        
        taskCompleteSound.Play();
        
        // Al completar todas las tareas, se abre la puerta para ganar.
        if (tasksCompleteCount == tasksExteriorState.Count)
        {
            openExitDoor = true;
        }
    }

    // Si el dron se deshabilita se bloqueará su movimiento y se activará una cuenta atras para su reactivación.
    public void DronEnabled(bool state)
    {
        dronEnabled = state;
        if (!dronEnabled || !terminalOn)
        {
            dron.GetComponent<DronController>().enabled = false;
        }
        else if (dronEnabled && terminalOn)
        {
            dron.GetComponent<DronController>().enabled = true;
        }
        
        
        if (state == true)
        {
            dronDisabledSoundEffect.Stop();
            dron.tag = "Dron";
            //dron.GetComponent<MeshRenderer>().material = dronEnabledMaterial;
            dronEnableTimer = 0;

            
            enemyEventsManager.PrepareNextEnemyEvent();
            
        }
        else
        {
            dronDisabledSoundEffect.Play();
            dron.tag = "DronDisabled";
            //dron.GetComponent<MeshRenderer>().material = dronDisabledMaterial;
            

            scoreManager.totalDronCrashes++;
        }
        
    }
    
}
