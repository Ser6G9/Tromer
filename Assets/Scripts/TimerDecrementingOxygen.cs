using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDecrementingOxygen : MonoBehaviour
{
    public float timer;
    public float time;
    void Start()
    {
        
    }

    void Update()
    {
        /*if (timer > 0.2f) // Input.GetKey(KeyCode.Space)
        {
            timer -= Time.deltaTime;
            transform.Translate(Vector3.down * 1 * Time.deltaTime);
        }*/
        
        // -----------------------
        
        timer -= Time.deltaTime;
        time += Time.deltaTime;
        if (time >= 1.0f && timer > 0.0f) // Se reducirá 0.06 metros cada vez que pase 1 segundo
        {
            transform.Translate(Vector3.down * 0.0666666666667f);
            time = 0;
        }
        else
        {
            // Game Over
        }
        
    }
}
