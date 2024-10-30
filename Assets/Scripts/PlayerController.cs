using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public float speed;
    public float rotationSpeed;
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
        verticalInput = Input.GetAxis("Vertical");
        // Calcular la dirección del movimiento
        transform.Translate(Vector3.forward * -horizontalInput * Time.deltaTime * speed); 
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.back * -verticalInput * Time.deltaTime * speed); // Movimiento diagonal debido a la perspectiva de la camara.
        transform.Translate(Vector3.left * -verticalInput * Time.deltaTime * speed);

       
            //transform.LookAt(transform.position + new Vector3(verticalInput * -horizontalInput, 0, horizontalInput + verticalInput));
    }
}
