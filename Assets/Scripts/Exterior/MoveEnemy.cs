using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Exterior
{
    public class MoveEnemy : MonoBehaviour
    {
        public float enemySpeed;
        public Transform target;
        private NavMeshAgent navMeshAgent;
        // Start is called before the first frame update
        void Start()
        {
            // Obtener el componente NavMeshAgent
            navMeshAgent = GetComponent<NavMeshAgent>();

            // Asignar la velocidad al NavMeshAgent
            navMeshAgent.speed = enemySpeed;
        }

        void Update()
        {
            // Establecer el destino del agente en cada frame
            navMeshAgent.destination = target.position;
        }
    }
}

