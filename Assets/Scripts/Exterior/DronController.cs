using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class DronController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public float speed;
    public float rotationSpeed;
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
        
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
        
        // Detectar el movimiento del raton:
        /*float mouseX = Input.GetAxis("Mouse X");
        Debug.Log(mouseX);*/
    }
}
