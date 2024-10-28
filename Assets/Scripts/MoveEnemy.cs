using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float enemySpeedX = 10; //Se moverá 10 metro por segundo
    public float enemySpeedZ = 10;
    
    public float enemyWith = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3(enemySpeedX * Time.deltaTime, 0, 0);
        transform.Translate(enemySpeedX * Time.deltaTime, 0, 0);

        if (transform.position.x + enemyWith >= 10 || transform.position.x - enemyWith <= -10)
        {
            enemySpeedX *= -1;
        }
        
    }
}
