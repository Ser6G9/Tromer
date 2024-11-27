using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowDron : MonoBehaviour
{
    private TromerLevelManager levelManager;
    private void OnEnable()
    {
        levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
    }
    public float xAxisCamera;
    public float rotationSpeed;
    public float maxYRotation;
    public float minYRotation;

    private float targetYAngle;
    private float currentYAngle;

    void Update()
    {
        // Calcular la dirección hacia el jugador
        Vector3 direction = levelManager.dron.transform.position - transform.position;
        direction.y = 0;
        
        // Se calcula la rotación deseada hacia el dron
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float targetYAngle = targetRotation.eulerAngles.y;

        // Me aseguro de que el ángulo se mantenga en el rango -180 a 180 para una comparación correcta
        if (targetYAngle > 180)
            targetYAngle -= 360;

        if (targetYAngle >= minYRotation && targetYAngle <= maxYRotation)
        {
            Quaternion rotation = Quaternion.Euler(xAxisCamera, targetYAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        
    }
}
