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
    public float oxigenMaxTime;
    public float timerForOneSecond;

    public TextMeshProUGUI oxigenProgress;
    public Slider oxigenSliderProgress;
    public GameObject oxigenLevel3DProgress;

    
    private void Start()
    {
        //youWin.SetActive(false);
        youLose.SetActive(false);
        PlayerChangeToTerminalMode(false);
    }

    private void Update()
    {
        OxigenCountDawnProgress();
        UpdateCoinsText();
        
    }
    
    
    public void UpdateCoinsText()
    {
        coinsText.text = coins.ToString();
    }

    // Cambiar cámara y controles del player al modo Terminal
    public void PlayerChangeToTerminalMode(bool state)
    {
        terminalCamera.gameObject.SetActive(state);
        dron.GetComponent<DronController>().enabled = state;
        
        roomPlayerCamera.gameObject.SetActive(!state);
        roomPlayer.GetComponent<PlayerController>().enabled = !state;
        for (int i = 0; i < roomPlayer.transform.childCount; i++) // También se desactivan/activan los componentes de Player
        {
            roomPlayer.transform.GetChild(i).gameObject.SetActive(!state);
        }
        terminalOn = state;
    }

    public void OxigenCountDawnProgress()
    {
        oxigenMaxTime -= Time.deltaTime;
        timerForOneSecond += Time.deltaTime;
        if (timerForOneSecond >= 1.0f && oxigenMaxTime > 0.0f) 
        {
            // Se reducirá 0.06 metros cada vez que pase 1 segundo
            oxigenLevel3DProgress.transform.Translate(Vector3.down * 0.0666666666667f);
            timerForOneSecond = 0;
        }
        else if (oxigenMaxTime <= 0.0f)
        {
            // Si el tiempo ha llegado a 0 entonces: Game Over
            GameOver();
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
