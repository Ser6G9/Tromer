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
                saveScoreText.text = "Puntuación guardada:\n \n"+ playerName +"\n" + totalScore+" puntos";
                buttonSaveScore.interactable = false;
                statsScoreText.gameObject.SetActive(false);
            } 
            else
            {
                Debug.LogError("Error en la petición: " + httpClient.error);
                saveScoreText.gameObject.SetActive(true);
                saveScoreText.text = "Error\nNo se pudo guardar la puntuación";
                statsScoreText.text = "\n\n\n\n\n\n\n" +
                                      "Puntuación total: " + gameManager.score;
            }
        }
    }

    public void ShowScoreStats()
    {
        saveScoreText.gameObject.SetActive(false);
        statsScoreText.text = "Tiempo de juego: "+ scoreManager.totalPlayTime +" segundos\n"+
                              "· "+ scoreManager.totalOxigenIncrementations +"% de oxígeno repuesto manualmente\n"+
                              "· "+ gameManager.tasksCompleteCount +"  tareas completadas\n"+
                              "· "+ gameManager.optiTasksCompleteCount +"  tareas extra completadas\n"+
                              "· "+ scoreManager.totalEmergencysFixed +"  brechas de gas selladas\n"+
                              "Veces que el Dron se ha averiado: "+ scoreManager.totalDronCrashes +"\n"+
                              "\nPuntuación total: " + gameManager.score;
    }
}
