using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float enemySpeed = 10; //Se moverá 10 metro por segundo
    public float maxDistance = 8;
    
    public float enemyWith = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3(enemySpeedX * Time.deltaTime, 0, 0);
        

        if (transform.position.x + enemyWith >= maxDistance)
        {
            transform.Translate(0, 0, enemySpeed * Time.deltaTime);
        } 
        if (transform.position.x + enemyWith <= -maxDistance)
        {
            transform.Translate(0, 0, -enemySpeed * Time.deltaTime);
        } 
        if (transform.position.z + enemyWith >= maxDistance)
        {
            transform.Translate(-enemySpeed * Time.deltaTime, 0, 0);
        } 
        if (transform.position.z + enemyWith <= -maxDistance)
        {
            transform.Translate(enemySpeed * Time.deltaTime, 0, 0);
        }
        
    }
}
