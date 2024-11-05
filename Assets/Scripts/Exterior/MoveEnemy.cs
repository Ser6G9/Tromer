using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveEnemy : MonoBehaviour
{
    public float enemySpeed;
    public float rotationSpeed;
    public int routine;
    public float i;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Movimiento_Random()
    {
        i += 1 * Time.deltaTime;
        if (i >= 4)
        {
            routine = Random.Range(0, 5);
            i = 0;
        }

        switch (routine)
        {
            case 0:
                // No hace nada
                break;
            case 1:
                // Se mueve hacia arriba
                if (transform.position.x <= 50)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), rotationSpeed * Time.deltaTime);
                    transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                }
                break;
            case 2:
                // Se mueve hacia abajo
                if (transform.position.x >= 0)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), rotationSpeed * Time.deltaTime);
                    transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                }
                break;
            case 3:
                // Se mueve hacia derecha
                if (transform.position.z >= -25)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), rotationSpeed * Time.deltaTime);
                    transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                }
                break;
            case 4:
                // Se mueve hacia izquierda
                if (transform.position.z <= 15)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), rotationSpeed * Time.deltaTime);
                    transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                }
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Movimiento_Random();
        
    }
}
