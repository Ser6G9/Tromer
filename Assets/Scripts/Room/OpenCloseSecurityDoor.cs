using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseSecurityDoor : MonoBehaviour
{
    private TromerLevelManager levelManager;
    private void OnEnable()
    {
        levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
    }
    
    public void OpenDoor()
    {
        // Se cierran el resto de puertas.
        for (int i = 0; i < levelManager.securityDoors.Count; i++)
        {
            if (levelManager.securityDoors[i] != this.gameObject)
            {
                levelManager.securityDoorsButtons[i].GetComponent<Image>().color = Color.red;
                levelManager.securityDoors[i].gameObject.SetActive(true);
            } 
            else if(levelManager.securityDoors[i] == this.gameObject)
            {
                // Se abre y se cambia el color del botón de la puerta que está abierta.
                this.gameObject.SetActive(false);
                levelManager.securityDoorsButtons[i].GetComponent<Image>().color = Color.green;
            }
        }
    }
}
