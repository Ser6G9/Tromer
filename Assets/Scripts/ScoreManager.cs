using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int totalScore = 0;
    
    // Estadistcas totales:
    public int totalPlayTime;
    public int totalOxigenIncrementations;
    public int totalCamerasFixed;
    public int totalEmergencysFixed;
    
    public int totalDronCrashes;
    
    // -- Gestores de puntuaciónes --
    public int maxPlayTimeScore = 3000; // a menor tiempo más puntuación
    //public int minPlayTimeScore = 0; // a mayor tiempo menor puntuación
    public float maxPlayTimeToEnd = 600; // por defecto 10 minutos minimo para ganar puntuación por tiempo.
    
    public int winGameScore = 1000;
    public int oxigenIncrementationScore = 1; // Por cada 1% que se suba el Oxigeno
    public int taskCompleteScore = 150;
    public int cameraFixedScore = 50;
    public int emergencyFixedScore = 75;
    
    // Restan puntuación:
    public int dronCrashesScore = 50;


    private float playTimer;
    
    private GameManager gameManager;
    private void OnEnable()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Contador de tiempo de juego
        if (gameManager.CheckIfGameIsPaused() == false)
        {
            playTimer += Time.deltaTime;
            totalPlayTime = Mathf.FloorToInt(playTimer);
        }

        totalScore = CalculateTotalScore(false);
    }
    
    public int CalculateTotalScore(bool gameWin)
    {
        totalScore = 0;
        // Puntuación por Ganar:
        if (gameWin)
        {
            totalScore += winGameScore;
        }
        
        // Puntuación por tiempo:
        if (totalPlayTime <= maxPlayTimeToEnd)
        {
            float timePercentage = 1f - (totalPlayTime / maxPlayTimeToEnd);
            totalScore += Mathf.FloorToInt(maxPlayTimeScore * timePercentage);
        }
        
        // Puntuación por Tareas completadas:
        totalScore += gameManager.tasksCompleteCount * taskCompleteScore;
        
        // Puntuación por incrementar Oxigeno:
        totalScore += (oxigenIncrementationScore * totalOxigenIncrementations);
        
        // Puntuación por reparar brechas de gas:
        totalScore += totalEmergencysFixed * emergencyFixedScore;
        
        // Penalización de puntuación por averías del Dron:
        totalScore -= totalDronCrashes * dronCrashesScore;
        
        return totalScore;
    }
}
