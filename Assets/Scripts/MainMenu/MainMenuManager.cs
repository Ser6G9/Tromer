using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public GameObject mainMenuPanel;
        public GameObject loginPanel;
        public GameObject registerPanel;
        public string userName;
        public TextMeshProUGUI userNameText;
        public TextMeshProUGUI scoreBoardText;

        public void Start()
        {
            ChangeLoginPanel();
            LeaderBoard leaderboard = FindObjectOfType<LeaderBoard>();
            leaderboard.GetLeaderBoard();
        }

        public void Update()
        {
            
        }

        public void UserName(string name)
        {
            userName = name;
            userNameText.text = userName;
        }
        
        public void ChangeMainMenuPanel()
        {
            mainMenuPanel.SetActive(true);
            loginPanel.SetActive(false);
            registerPanel.SetActive(false);
        }
        public void ChangeLoginPanel()
        {
            loginPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
            registerPanel.SetActive(false);
        }
        public void ChangeRegisterPanel()
        {
            loginPanel.SetActive(false);
            mainMenuPanel.SetActive(false);
            registerPanel.SetActive(true);
        }
    }
}

