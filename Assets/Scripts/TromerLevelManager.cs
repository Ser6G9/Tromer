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
    public bool consoleOn;
    public GameObject roomPlayerCamera;
    public GameObject roomPlayer;
    public GameObject consoleCamera;
    public GameObject dron;
    
    // Controles de las camaras de seguridad.
    public List<GameObject> securityCameras;
    public List<GameObject> securityCamerasScreens;
    public List<GameObject> securityCamerasButtons;
    

    private void Start()
    {
        PlayerChangeToConsoleMode(false);
    }

    private void Update()
    {
        UpdateCoinsText();
        
    }

    
    public void UpdateCoinsText()
    {
        coinsText.text = coins.ToString();
    }

    public void PlayerChangeToConsoleMode(bool state)
    {
        consoleCamera.gameObject.SetActive(state);
        // consoleControls.gameObject.SetActive(state);
        dron.GetComponent<DronController>().enabled = state;
        
        roomPlayerCamera.gameObject.SetActive(!state);
        roomPlayer.GetComponent<PlayerController>().enabled = !state;
        for (int i = 0; i < roomPlayer.transform.childCount; i++)
        {
            roomPlayer.transform.GetChild(i).gameObject.SetActive(!state);
        }
        consoleOn = state;
    }
}
