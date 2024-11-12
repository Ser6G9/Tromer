using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exterior
{
    public class CollectCoin : MonoBehaviour
    {
    
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player") // "Player" es un Tag que se le ha asignado al player desde el inspector.
            {
                Debug.Log("Collect coin");
                Destroy(this.gameObject);
            }
        }
    }
}

