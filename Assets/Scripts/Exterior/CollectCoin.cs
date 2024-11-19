using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exterior
{
    public class CollectCoin : MonoBehaviour
    {
        private TromerLevelManager levelManager;

        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player") // "Player" es un Tag que se le ha asignado al player desde el inspector.
            {
                Debug.Log(levelManager.coins);
                levelManager.coins++;
                
                Destroy(this.gameObject);
            }

            if (levelManager.coins >= 3)
            {
                Debug.Log("You win");
            }
        }
    }
}

