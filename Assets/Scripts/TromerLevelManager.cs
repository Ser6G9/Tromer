using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TromerLevelManager : MonoBehaviour
{
    public GameObject youWin;
    
    // Cada vez que el dron recoja una coin, el HUD sumará +1 al contador
    public TextMeshProUGUI coinsText;
    public int coins = 0; 
    
    // Para cambiar de modos de juego.
    public GameObject roomPlayerCamera;
    public GameObject roomPlayer;
    public GameObject consoleCamera;
    // public GameObject consoleControls;
    public GameObject dron;

    public void UpdateCoinsText()
    {
        coinsText.text = coins.ToString();
    }

    public void PlayerControlTerminalMode(bool state)
    {
        consoleCamera.gameObject.SetActive(state);
        // consoleControls.gameObject.SetActive(state);
        dron.gameObject.SetActive(state);
        
        roomPlayerCamera.gameObject.SetActive(!state);
        roomPlayer.gameObject.SetActive(!state);
    }

    public void ChangeToRoomMode()
    {
        if (consoleCamera.gameObject.activeSelf == true && Input.GetKey(KeyCode.Space))
        {
            PlayerControlTerminalMode(false);
        }
    }

    /*private void Start()
    {
        PlayerControlTerminalMode(false);
    }*/

    private void Update()
    {
        UpdateCoinsText();
        ChangeToRoomMode();
    }
    
    
}
