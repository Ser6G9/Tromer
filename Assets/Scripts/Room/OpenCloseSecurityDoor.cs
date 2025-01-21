using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseSecurityDoor : MonoBehaviour
{
    private TromerLevelManager levelManager;
    private void OnEnable()
    {
        levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
    }
    
    public bool openDoor = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor(openDoor);
    }

    public void OpenDoor(bool open)
    {
        // Se abre la puerta seleccionada y se cierran el resto de puertas.
        for (int i = 0; i < levelManager.securityDoors.Count; i++)
        {
            if (levelManager.securityDoors[i] != this.gameObject)
            {
                //levelManager.botonesVerdes[i].gameObject.SetActive(false);
                //levelManager.botonesRojos[i].gameObject.SetActive(true);
                levelManager.securityDoors[i].gameObject.SetActive(true);
            } 
            else if(levelManager.securityDoors[i] == this.gameObject && open)
            {
                // Se abre y se cambia el color del botón de la puerta abierta a verde.
                this.gameObject.SetActive(open);
                //levelManager.botonesVerdes[i].gameObject.SetActive(true);
                //levelManager.botonesRojos[i].gameObject.SetActive(false);
            }
        }
    }
}
