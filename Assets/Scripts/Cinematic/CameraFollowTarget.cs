using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
   
    private Transform target; // El objeto a seguir
    private float speed = 5f; // Velocidad de seguimiento
    private Vector3 offset; // Offset opcional para ajustar la posición de la cámara

    private void LateUpdate()
    {
        if (target != null)
        {
            // Ajusta la posición con el offset
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
            
            // Hace que la cámara siempre mire al target
            transform.LookAt(target);
        }
    }
}
