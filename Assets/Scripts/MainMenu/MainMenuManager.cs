using System.Collections;
using System.Collections.Generic;
using MainMenu.DTO;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public User User;
        public GameObject mainMenuPanel;
        public GameObject loginPanel;
        public GameObject registerPanel;
        public TextMeshProUGUI userNameText;
        public TextMeshProUGUI scoreBoardText;

        public void Start()
        {
            /* Escena LoginRegisterMenu:
            ChangeLoginPanel();
            LeaderBoard leaderboard = FindObjectOfType<LeaderBoard>();
            leaderboard.GetLeaderBoard();*/
            
            ScoreBoard leaderboard = FindObjectOfType<ScoreBoard>();
            leaderboard.GetScoreBoard();
            Time.timeScale = 1f;
        }

        public void Update()
        {
            
        }

        
        
        // --- Escena LoginRegisterMenu: ---
        public void UserData(User u)
        {
            User = u;
            if (u != null)
            {
                userNameText.text = User.name;
            }
            else
            {
                userNameText.text = null;
            }
            
        }

        public void Logout()
        {
            UserData(null);
            ChangeLoginPanel();
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

