using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SaveScore : MonoBehaviour
{
    public NetworkingDataScriptableObject apiData;
    [System.Serializable]
    public class ScoreData
    {
        public string api_token;
        public string name;
        public int puntuacion;
    }
    
    public TMP_InputField userName;
    public TextMeshProUGUI saveScoreText;
    
    private GameManager gameManager;
    private void OnEnable()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void SaveTotalScore()
    {
        StartCoroutine(SendScore(userName.text, gameManager.score));
    }
    
    private IEnumerator SendScore(string playerName, int totalScore)
    {
        // Crear el objeto de datos
        ScoreData datos = new ScoreData()
        {
            api_token = apiData.token,
            name = playerName,
            puntuacion = totalScore
        };
        
        string url = apiData.apiUrl + "/api/classification";
        
        string json = JsonConvert.SerializeObject(datos);

        // Crear la solicitud
        using (UnityWebRequest httpClient = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
            httpClient.uploadHandler = new UploadHandlerRaw(jsonToSend);
            httpClient.downloadHandler = new DownloadHandlerBuffer();
            httpClient.SetRequestHeader("Content-Type", "application/json");
            Debug.Log(url);

            // Enviar y esperar la respuesta
            yield return httpClient.SendWebRequest();

            if (httpClient.result == UnityWebRequest.Result.Success)
            {
                Debug.LogError("Respuesta de la API: " + httpClient.downloadHandler.text);
                saveScoreText.text = "Puntuación de "+ playerName +": " + totalScore;
            } 
            else
            {
                Debug.LogError("Error en la petición: " + httpClient.error);
                saveScoreText.text = "No se pudo guardar la puntuación.";
            }
        }
    }
}
