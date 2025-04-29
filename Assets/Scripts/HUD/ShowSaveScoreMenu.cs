using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSaveScoreMenu : MonoBehaviour
{
    public GameObject scoreMenu;
    
    private GameManager gameManager;
    private void OnEnable()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void StartSaveScoreMenu()
    {
        scoreMenu.SetActive(true);
        gameManager.saveScoreMenuOn = true;
    }
}
