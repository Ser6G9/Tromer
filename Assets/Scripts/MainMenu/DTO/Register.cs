using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace MainMenu.DTO
{
    public class Register : MonoBehaviour
    {
        public User user;
        public NetworkingDataScriptableObject loginDataSO;
        public TMP_InputField nameInput;
        public TMP_InputField emailInput;
        public TMP_InputField passwordInput;
        
        public void RegisterUser()
        {
            Debug.Log("Sign in...");
            StartCoroutine(TryRegister());
        }
    
        private IEnumerator TryRegister()
        {
            if (user == null)
            {
                UnityWebRequest httpClient = new UnityWebRequest();
                httpClient.method = UnityWebRequest.kHttpVerbPOST;
                httpClient.url = loginDataSO.apiUrl + "/Auth/Register";
                httpClient.SetRequestHeader("Content-Type", "application/json");
                httpClient.SetRequestHeader("Accept", "application/json");
    
                RegisterUserDTO loginDataUser = new RegisterUserDTO();
                loginDataUser.Name = nameInput.text;
                loginDataUser.Email = emailInput.text;
                loginDataUser.Password = passwordInput.text;
                
                string jsonData = JsonConvert.SerializeObject(loginDataUser);
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
            }
        }
    }

}
