using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public float speed;
    public float xRange ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Controles de movimiento:
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * -horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.back * -verticalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.left * -verticalInput * Time.deltaTime * speed);
    }
}
