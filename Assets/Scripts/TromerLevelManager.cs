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
    
    // Cada vez que el dron recoja una coin, el HUD sumará +1 al contador
    public TextMeshProUGUI coinsText;
    public int coins = 0; 
    
    // Para cambiar de modos/pantallas de juego:
    public bool terminalOn;
    public GameObject roomPlayerCamera;
    public GameObject roomPlayer;
    public GameObject terminalCamera;
    public GameObject dron;
    
    // Controles de las camaras de seguridad:
    public List<GameObject> securityCameras;
    public List<GameObject> securityCamerasScreens;
    public List<GameObject> securityCamerasButtons;
    
    // Gestión del contador de Oxígeno:
    public float totalOxigenTime;
    public float oxigenProgressTime;
    public bool oxigenIncrementationOn = false;
    public float oxigenIncrementationSpeed;
    public TextMeshProUGUI oxigenProgressText;
    public Slider oxigenSliderProgress;
    public GameObject oxigenLevel3DProgress;
    public float oxigenPercentage = 100;
    
    private void Start()
    {
        //youWin.SetActive(false);
        youLose.SetActive(false);
        
        oxigenProgressTime = totalOxigenTime;
        ShowOxigenLevelProgress();
        
        PlayerChangeToTerminalMode(false);
    }

    private void Update()
    {
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
        
    }
    
    
    public void UpdateCoinsText()
    {
        coinsText.text = coins.ToString();
    }

    // Cambiar cámara y controles del player al modo Terminal:
    public void PlayerChangeToTerminalMode(bool state)
    {
        terminalCamera.gameObject.SetActive(state);
        dron.GetComponent<DronController>().enabled = state;
        
        roomPlayerCamera.gameObject.SetActive(!state);
        roomPlayer.gameObject.SetActive(!state);
        for (int i = 0; i < roomPlayer.transform.childCount; i++) // También se desactivan/activan los componentes del GameObject Player
        {
            roomPlayer.transform.GetChild(i).gameObject.SetActive(!state);
        }
        terminalOn = state;
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

    public void GameOver()
    {
        youLose.SetActive(true);
    }
    public void GameWin()
    {
        //youWin.SetActive(true);
    }
}
