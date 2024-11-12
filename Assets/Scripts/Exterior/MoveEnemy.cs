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
            navMeshAgent = GetComponent<NavMeshAgent>();
            
        }
    
        // Update is called once per frame
        void Update()
        {
            navMeshAgent.destination = target.position;
        }
    }
}

