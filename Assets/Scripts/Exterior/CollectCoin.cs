using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exterior
{
    public class CollectCoin : MonoBehaviour
    {
        private GameManager levelManager;

        private void OnEnable()
        {
            levelManager = GameObject.FindObjectOfType<GameManager>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Dron") // "Player" es un Tag que se le ha asignado al player desde el inspector.
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

