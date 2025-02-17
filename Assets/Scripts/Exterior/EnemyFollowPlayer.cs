using System;
using System.Collections;
using System.Collections.Generic;
using Room;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Exterior
{
    public class EnemyFollowPlayer : MonoBehaviour
    {
        private TromerLevelManager levelManager;
        private ConsoleMinimap ConsoleMinimap;
        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
            ConsoleMinimap = GameObject.FindObjectOfType<ConsoleMinimap>();
        }
        
        public float enemySpeed;
        public Transform target;
        private NavMeshAgent navMeshAgent;
        public List<GameObject> spawn;
        
        
        void Start()
        {
            // Obtener el componente NavMeshAgent
            navMeshAgent = GetComponent<NavMeshAgent>();

            // Asignar la velocidad al NavMeshAgent
            navMeshAgent.speed = enemySpeed;

            SpawnEnemy();
        }

        void Update()
        {
            // Establecer el destino del enemy en cada frame
            navMeshAgent.destination = target.position;
        }

        // Al colisionar con el Dron, el dron se deshabilita y el enemy desaparece.
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Dron"))
            {
                this.gameObject.SetActive(false);
                ConsoleMinimap.enemyMarker.gameObject.SetActive(false);
                levelManager.DronEnabled(false);
            }
        }

        public void SpawnEnemy()
        {
            this.gameObject.transform.position = spawn[Random.Range(0, spawn.Count)].transform.position;
            this.gameObject.SetActive(true);
            ConsoleMinimap.enemyMarker.gameObject.SetActive(true);
        }
        
    }
}

