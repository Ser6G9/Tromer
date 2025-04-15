using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreBoard : MonoBehaviour
{
    public NetworkingDataScriptableObject apiData;
    public TextMeshProUGUI playersBoardText;
    public TextMeshProUGUI scoreBoardText;

    private List<ScoreBoardDto> scoreBoardList = new List<ScoreBoardDto>();

    public void GetScoreBoard()
    {
        StartCoroutine(SetScoreBoard());
    }

    
    private IEnumerator SetScoreBoard()
    {
        string url = apiData.apiUrl + "/api/classification/" + apiData.token;

        using (UnityWebRequest httpClient = UnityWebRequest.Get(url))
        {
            httpClient.SetRequestHeader("Accept", "application/json");
            Debug.Log(url);

            yield return httpClient.SendWebRequest();

            if (httpClient.result == UnityWebRequest.Result.ConnectionError || httpClient.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la petición: " + httpClient.error);
                playersBoardText.text = "No se pudo obtener la clasificación.";
                yield break;
            }

            string jsonResponse = httpClient.downloadHandler.text;

            try
            {
                // Convertimos el JSON en una lista de objetos
                var response = JsonConvert.DeserializeObject<ScoreBoardResponse>(jsonResponse);
                scoreBoardList = response.Data;
                ShowScoreBoard();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error al deserializar JSON: " + ex.Message);
                playersBoardText.text = "Error al cargar la clasificación.";
            }
        }
    }
    
    private void ShowScoreBoard()
    {
        playersBoardText.text = "";
        scoreBoardText.text = "";
        int count = 0;
        if (scoreBoardList.Count > 0)
        {
            foreach (var player in scoreBoardList)
            {
                count++;
                if (count <= 6)
                {
                    playersBoardText.text += $"{count}. {player.Name}\n";
                    scoreBoardText.text += $"-  {player.Puntuacion}\n";
                    if (count == 6)
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            scoreBoardText.text = "No hay jugadores en la clasificación.";
        }
    }
}
