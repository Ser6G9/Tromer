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
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<NavMeshAgent>().destination = target.position;
        }
    
        // Update is called once per frame
        void Update()
        {
            GetComponent<NavMeshAgent>().destination = target.position;
        }
    }
}

