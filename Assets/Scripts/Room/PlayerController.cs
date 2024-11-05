using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        transform.position += movement * speed * Time.deltaTime;
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(movement),rotationSpeed * Time.deltaTime);
        }
        
        // Detectar el movimiento del raton:
        /*float mouseX = Input.GetAxis("Mouse X");
        Debug.Log(mouseX);*/
    }
}
