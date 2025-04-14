using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronMenuAnimation : MonoBehaviour
{
    /*/*public Vector3 destination;
    public int action = 1;#1#
    public float speed = 3f;
    public float rotateSpeed = 3f;
    
    private Vector3 startPos;
    private int phase = 0;
    private float moved = 0f;
    private float rotated = 0f;
    private void OnEnable()
    {
        startPos = new Vector3(-10.44f, 0.5f, 21.22f);
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

    void Update()
    {
        if (action == 1)
        {
            destination = new Vector3(-1.565f, 0.5f, 21.22f);
            transform.position = Vector3.MoveTowards(transform.position, destination, 2f * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination) != 0.0f)
            {
                action = 2;
            }
        }

        if (action == 2)
        {
            destination = new Vector3(0.298f, 0.5f, 13.078f);
            //transform.rotation = Quaternion.Euler(0,159.665f,0);
            transform.position = Vector3.MoveTowards(transform.position, destination, 2f * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination) != 0.0f)
            {
                
                action = 3;
            }
        }
       
    

        
        
    }*/
    
    public float moveSpeed = 3f;
    public float moveDuration = 2f; // Tiempo que dura moviéndose en cada tramo
    public float rotationSpeed = 100f; // Grados por segundo al girar

    private void OnEnable()
    {
        transform.position = new Vector3(-9.4f, 0.5f, 21.22f);
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        StartCoroutine(MoveSequence());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveSequence()
    {
        // 1. Mover recto
        yield return MoveForward(moveDuration+1);

        // 2. Girar a la derecha (90°)
        yield return Rotate(70f);

        // 3. Mover recto
        yield return MoveForward(moveDuration);

        // 4. Girar a la izquierda (-90°)
        yield return Rotate(-90f);

        // 5. Mover recto
        yield return MoveForward(moveDuration);
    }

    IEnumerator MoveForward(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Rotate(float angle)
    {
        float rotated = 0f;
        float direction = Mathf.Sign(angle);

        while (Mathf.Abs(rotated) < Mathf.Abs(angle))
        {
            float step = rotationSpeed * Time.deltaTime * direction;
            transform.Rotate(Vector3.up, step);
            rotated += step;
            yield return null;
        }

        // Ajuste final para evitar errores por acumulación de flotantes
        Vector3 euler = transform.rotation.eulerAngles;
        euler.y = Mathf.Round(euler.y / 90f) * 90f;
        transform.rotation = Quaternion.Euler(euler);
    }
    
}
