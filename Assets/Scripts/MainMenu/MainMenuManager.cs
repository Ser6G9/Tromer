using System;
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
        public GameObject mainCamera;
        public GameObject terminalCamera;
        public GameObject menuPanel;
        public GameObject tutorialPanel;
        public GameObject scoreGuidePanel;
        public TextMeshProUGUI guideScoresText;
        public TextMeshProUGUI scoreGuideScoresText;
        
        private ScoreManager scoreManager;
        private void OnEnable()
        {
            scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        }

        public void Start()
        {
            /* Escena LoginRegisterMenu:
            ChangeLoginPanel();
            LeaderBoard leaderboard = FindObjectOfType<LeaderBoard>();
            leaderboard.GetLeaderBoard();*/
            
            ScoreBoard leaderboard = FindObjectOfType<ScoreBoard>();
            leaderboard.GetScoreBoard();
            Time.timeScale = 1f;
            
            ShowTutorial(false);
            ShowScoreGuide(false);
            
            updateScoresGuide();
        }

        public void PlaySound(AudioSource audioSource)
        {
            audioSource.Play();
        }

        public void ShowTutorial(bool show)
        {
            mainCamera.SetActive(!show);
            menuPanel.SetActive(!show);
            terminalCamera.SetActive(show);
            tutorialPanel.SetActive(show);
        }
        
        public void ShowScoreGuide(bool show)
        {
            mainCamera.SetActive(!show);
            menuPanel.SetActive(!show);
            terminalCamera.SetActive(show);
            scoreGuidePanel.SetActive(show);
        }

        public void updateScoresGuide()
        {
            guideScoresText.text = "- Ganas la partida: \n" +
                                   "- Se completa una tarea: \n" +
                                   "- Se completa una tarea extra: \n" +
                                   "- Se repara una brecha de gas: \n" +
                                   "- Por cada 1% de oxígeno que se reponga manualmente: ";
            
            scoreGuideScoresText.text = "+"+scoreManager.winGameScore+" puntos\n" +
                                        "+"+scoreManager.taskCompleteScore+" puntos por cada tarea\n" +
                                        "+"+scoreManager.optiTaskCompleteScore+" puntos\n" +
                                        "+"+scoreManager.emergencyFixedScore+" puntos\n" +
                                        "\n+"+scoreManager.oxigenIncrementationScore+" punto";
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

