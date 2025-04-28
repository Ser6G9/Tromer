using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

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
    public TextMeshProUGUI statsScoreText;
    public Button buttonSaveScore;
    
    private GameManager gameManager;
    private ScoreManager scoreManager;
    private void OnEnable()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        ShowScoreStats();
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
                saveScoreText.gameObject.SetActive(true);
                saveScoreText.text = "Puntuación guardada:\n"+ playerName +" - " + totalScore;
                buttonSaveScore.interactable = false;
                statsScoreText.gameObject.SetActive(false);
            } 
            else
            {
                Debug.LogError("Error en la petición: " + httpClient.error);
                saveScoreText.gameObject.SetActive(true);
                saveScoreText.text = "Error\nNo se pudo guardar la puntuación";
                statsScoreText.gameObject.SetActive(false);
            }
        }
    }

    public void ShowScoreStats()
    {
        saveScoreText.gameObject.SetActive(false);
        statsScoreText.text = "Tiempo: "+ scoreManager.totalPlayTime +"\n"+
                              "Veces que se ha repuesto oxígeno: "+ scoreManager.totalOxigenIncrementations +"\n"+
                              "Tareas completadas: "+ gameManager.tasksCompleteCount +"\n"+
                              "Brechas de gas selladas: "+ scoreManager.totalEmergencysFixed +"\n"+
                              "Averías del Dron: "+ scoreManager.totalDronCrashes +"\n"+
                              "\nPuntuación total: " + gameManager.score;
    }
}
