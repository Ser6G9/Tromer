using System;
using System.Collections;
using System.Collections.Generic;
using Exterior;
using Room;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TromerLevelManager : MonoBehaviour
{
    public GameObject youLose;
    
    // Cada vez que el dron recoja una coin, el HUD sumará +1 al contador
    public TextMeshProUGUI coinsText;
    public int coins = 0; 
    
    // Para cambiar de modos de juego.
    public bool terminalOn;
    public GameObject roomPlayerCamera;
    public GameObject roomPlayer;
    public GameObject terminalCamera;
    public GameObject dron;
    
    // Controles de las camaras de seguridad.
    public List<GameObject> securityCameras;
    public List<GameObject> securityCamerasScreens;
    public List<GameObject> securityCamerasButtons;
    

    private void Start()
    {
        PlayerChangeToTerminalMode(false);
    }

    private void Update()
    {
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
        for (int i = 0; i < roomPlayer.transform.childCount; i++)
        {
            roomPlayer.transform.GetChild(i).gameObject.SetActive(!state);
        }
        terminalOn = state;
    }
}
