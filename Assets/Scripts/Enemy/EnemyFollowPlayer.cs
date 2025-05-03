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
        private GameManager levelManager;
        private ConsoleMinimap ConsoleMinimap;
        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<GameManager>();
            ConsoleMinimap = GameObject.FindObjectOfType<ConsoleMinimap>();
        }
        
        public float enemySpeed;
        public Transform target;
        private NavMeshAgent navMeshAgent;
        public List<GameObject> spawn;
        public Animator animator;
        public float destroyTime = 2f;
        public float destroyTimer;
        public bool enemyAtack;
        /*
        public float timeToDespawn = 10f;
        public float despawnTimer;
        */
        
        void Start()
        {
            // Obtener el componente NavMeshAgent
            navMeshAgent = GetComponent<NavMeshAgent>();

            // Asignar la velocidad al NavMeshAgent
            navMeshAgent.speed = enemySpeed;

            SpawnEnemy();
            destroyTimer = destroyTime;
            enemyAtack = false;

        }

        void Update()
        {
            // Establecer el destino del enemy en cada frame
            navMeshAgent.destination = target.position;
            animator.SetBool("isWalking", true);

            if (enemyAtack)
            {
                destroyTimer -= Time.deltaTime;
                if (destroyTimer <= 1.2f)
                {
                    levelManager.dronDisabledParticles.Play();
                }
                if (destroyTimer <= 0)
                {
                    EnemyAtackComplete();
                }
            }

            /*// Desaparecerá al cabo de unos segundos
            if (despawnTimer > 0)
            {
                despawnTimer -= Time.deltaTime;
                if (despawnTimer <= 0)
                {
                    EnemyAtackComplete();
                }
            }*/
        }

        // Al colisionar con el Dron, el dron se deshabilita y el enemy desaparece.
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Dron"))
            {
                animator.SetTrigger("isAttacking");
                enemyAtack = true;
                
                levelManager.DronEnabled(false);
            }
        }

        public void SpawnEnemy()
        {
            this.gameObject.transform.position = spawn[Random.Range(0, spawn.Count)].transform.position;
            this.gameObject.SetActive(true);
            ConsoleMinimap.enemyMarker.gameObject.SetActive(true);
            
            //despawnTimer = timeToDespawn;
        }

        public void EnemyAtackComplete()
        {
            this.gameObject.SetActive(false);
            ConsoleMinimap.enemyMarker.gameObject.SetActive(false);
            
            destroyTimer = destroyTime;
            enemyAtack = false;
        }
        
    }
}

