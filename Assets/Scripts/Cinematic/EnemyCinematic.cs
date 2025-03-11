using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCinematic : MonoBehaviour
{
    public float speed = 2;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
