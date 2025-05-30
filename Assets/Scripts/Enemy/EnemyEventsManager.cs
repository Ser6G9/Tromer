using System.Collections;
using System.Collections.Generic;
using Exterior;
using UnityEngine;

public class EnemyEventsManager : MonoBehaviour
{
    public GameObject enemy;
    public int currentRandomEnemyEvent = 1; // Empieza siempre en el evento 1 (perseguir al Dron)
    public float countDawnToNextEnemyEvent = 0;
    public float timeMinToNextEvent = 10f;
    public float timeMaxToNextEvent = 35f;
    
    private EnemyFollowPlayer enemyFollowPlayer;
    private GameManager gameManager;
    private void OnEnable()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        enemyFollowPlayer = GameObject.FindObjectOfType<EnemyFollowPlayer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PrepareNextEnemyEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareNextEnemyEvent()
    {
        // ProVISional (respawnea el Enemy)
        StartCoroutine(EsperarYSpawnear());
    }
    
    private IEnumerator EsperarYSpawnear()
    {
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        enemyFollowPlayer.SpawnEnemy();       // Luego llama a SpawnEnemy
    }
}
