using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoginData", menuName = "ScriptableObjects/NetworkingManagerScriptableObject", order = 1)]

public class NetworkingDataScriptableObject : ScriptableObject
{
    public string apiUrl = "https://tromer-api.azurewebsites.net/api";
    public string token;
}