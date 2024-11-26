using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private TromerLevelManager levelManager;
    private void OnEnable()
    {
        levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
    }
    public float XAxisCamera;
    public float rotationSpeed;
    public float maxYAngle;
    public float minYAngle;

    private float targetYAngle;
    private float currentYAngle;
    
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 direction = levelManager.dron.transform.position - this.transform.position;
        direction.y = 0;
        
        // Se calcula la rotación deseada hacia el dron
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        // Se verifica si el ángulo deseado está dentro del rango permitido
        if (targetRotation.eulerAngles.y > minYAngle && targetRotation.eulerAngles.y < maxYAngle)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        } 
        else
        {
            // Si no está dentro del rango, se ajusta la rotación al límite más cercano
            float clampedAngle = Mathf.Clamp(currentYAngle, minYAngle, maxYAngle);
            Quaternion clampedRotation = Quaternion.Euler(0, clampedAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, clampedRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
