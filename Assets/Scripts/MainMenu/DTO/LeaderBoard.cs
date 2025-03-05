using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MainMenu;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoard : MonoBehaviour
{
        public NetworkingDataScriptableObject apiData;
        public TextMeshProUGUI leaderboardText;
        
        private List<LeaderBoardDto> leaderBoardList = new List<LeaderBoardDto>();
        
        public void GetLeaderBoard()
        {
            StartCoroutine(GetClassificationLevel1());
        }
    
        private IEnumerator GetClassificationLevel1()
        {
            string url = apiData.apiUrl + "/LeaderBoardL1/GetClassificationLevel1";

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Accept", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + apiData.token); // Si la API requiere token

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error en la petición: " + request.error);
                    leaderboardText.text = "No se pudo obtener la clasificación.";
                    yield break;
                }

                string jsonResponse = request.downloadHandler.text;

                try
                {
                    // Convertimos el JSON en una lista de objetos
                    leaderBoardList = JsonConvert.DeserializeObject<List<LeaderBoardDto>>(jsonResponse);
                    ShowLeaderBoard();
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error al deserializar JSON: " + ex.Message);
                    leaderboardText.text = "Error al cargar la clasificación.";
                }
            }
        }

        private void ShowLeaderBoard()
        {
            leaderboardText.text = ""; // Limpiamos el texto antes de mostrar

            if (leaderBoardList.Count > 0)
            {
                foreach (var player in leaderBoardList)
                {
                    leaderboardText.text += $"{player.UserName}  -  {player.Score}\n \n";
                }
            }
            else
            {
                leaderboardText.text = "No hay jugadores en la clasificación.";
            }
        }
}
