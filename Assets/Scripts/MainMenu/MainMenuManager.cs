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
            ChangeLoginPanel();
            LeaderBoard leaderboard = FindObjectOfType<LeaderBoard>();
            leaderboard.GetLeaderBoard();
        }

        public void Update()
        {
            
        }

        public void UserData(User u)
        {
            User = u;
            userNameText.text = User.name;
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

