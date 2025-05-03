using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseSecurityDoor : MonoBehaviour
{
    private GameManager levelManager;
    private void OnEnable()
    {
        levelManager = GameObject.FindObjectOfType<GameManager>();
    }
    
    public AudioSource closeSound;
    
    public void OpenDoor()
    {
        closeSound.Stop();
        
        // Se cierran el resto de puertas.
        for (int i = 0; i < levelManager.securityDoors.Count; i++)
        {
            if (levelManager.securityDoors[i] != this.gameObject)
            {
                levelManager.securityDoorsButtons[i].GetComponent<Image>().color = new Color(1f, 0.1784818f, 0.06132078f, 1f);
                levelManager.securityDoors[i].gameObject.SetActive(true);
                closeSound.Play();
            } 
            else if(levelManager.securityDoors[i] == this.gameObject && this.gameObject.activeSelf)
            {
                // Se abre y se cambia el color del botón de la puerta que está abierta.
                this.gameObject.SetActive(false);
                levelManager.securityDoorsButtons[i].GetComponent<Image>().color = new Color(0.66f, 1f, 0.18f, 1f);
            } else if (levelManager.securityDoors[i] == this.gameObject && this.gameObject.activeSelf == false)
            {
                // Si ya está abierta, se cierra y se cambia el color del botón de la puerta.
                levelManager.securityDoorsButtons[i].GetComponent<Image>().color = new Color(1f, 0.1784818f, 0.06132078f, 1f);
                levelManager.securityDoors[i].gameObject.SetActive(true);
                closeSound.Play();
            }
        }
    }
}
