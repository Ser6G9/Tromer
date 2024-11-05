using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class MoveEnemy : MonoBehaviour
{
    public float enemySpeed;
    public float rotationSpeed;
    public int rutina;
    public float cronometro;
    public Quaternion angulo;
    public float grado;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Comportamento_Enemigo()
    {
        cronometro += 1 * Time.deltaTime;
        if (cronometro >= 4)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;
        }

        switch (rutina)
        {
            case 0:
                break;
            case 1:
                grado = Random.Range(0, 360);
                transform.Rotate(0, grado,0);
                rutina++;
                break;
            case 2:
                transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Comportamento_Enemigo();
        // transform.Translate(0, 0, enemySpeed * Time.deltaTime);
        
    }
}
