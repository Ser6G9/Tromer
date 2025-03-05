using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MainMenu;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GameLevelScore : MonoBehaviour
{
        public User user;
        public NetworkingDataScriptableObject loginDataSO;
        public TMP_InputField emailInput;
        public TMP_InputField passwordInput;
        
        public void GetScoreBoard()
        {
            Debug.Log("Getting scores...");
            StartCoroutine(GetScore());
        }
    
        private IEnumerator GetScore()
        {
            if (user == null)
            {
                UnityWebRequest httpClient = new UnityWebRequest();
                httpClient.method = UnityWebRequest.kHttpVerbPOST;
                httpClient.url = loginDataSO.apiUrl + "/LeaderBoardL1/GetClassificationLevel1";
                httpClient.SetRequestHeader("Content-Type", "application/json");
                httpClient.SetRequestHeader("Accept", "application/json");
    
                GameLevelDto GameLevelData = new GameLevelDto();
                GameLevelData.Id = "prueva"; 
                GameLevelData.UserId = emailInput.text;
                GameLevelData.UserName = passwordInput.text;
                
                string jsonData = JsonConvert.SerializeObject(GameLevelData);
                byte[] dataToSend = Encoding.UTF8.GetBytes(jsonData);
                
                httpClient.uploadHandler = new UploadHandlerRaw(dataToSend);
                httpClient.downloadHandler = new DownloadHandlerBuffer();
                
                
                yield return httpClient.SendWebRequest();
    
                if (httpClient.result == UnityWebRequest.Result.ConnectionError || httpClient.result == UnityWebRequest.Result.ProtocolError)
                {
                    throw new Exception("Login: " + httpClient.error);
                }
                
                string jsonResponse = httpClient.downloadHandler.text;
                
                UserDTO userdto = JsonConvert.DeserializeObject<UserDTO>(jsonResponse);
                loginDataSO.token = userdto.token;
                Debug.Log(userdto.token);
                user = new User(userdto.id, userdto.name, userdto.email,userdto.token);
                httpClient.Dispose();

                MainMenuManager manager = GameObject.FindObjectOfType<MainMenuManager>();
                manager.ChangeMainMenuPanel();
                manager.UserName(user.name);
                
            }
        }
}
