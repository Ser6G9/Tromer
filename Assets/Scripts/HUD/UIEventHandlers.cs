using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UIEventHandlers : MonoBehaviour
{
    
    
    public void Login()
    {
        Debug.Log("Login...");
        StartCoroutine(TryLogin());
    }

    private IEnumerator TryLogin()
    {
        if (user == null)
        {
            UnityWebRequest httpClient = new UnityWebRequest();
            httpClient.method = UnityWebRequest.kHttpVerbPOST;
            httpClient.url = loginDataSO.apiURL + "/Auth/Login";
            httpClient.SetRequestHeader("Content-Type", "application/json");
            httpClient.SetRequestHeader("Accept", "application/json");

            RegisterUserDTO loginDataUser = new RegisterUserDTO();
            loginDataUser.Name = "prueva"; // NO puede ser null.
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
            
            AuthTokenDto authTokenDto = JsonConvert.DeserializeObject<AuthTokenDto>(jsonResponse);
            loginDataSO.token = authTokenDto.token;
            Debug.Log(authTokenDto.token);
            httpClient.Dispose();
        }
    }
    
    public void ExitGameButton()
    {
        // Salir de la aplicación.
        Application.Quit();
    }

    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    
}
