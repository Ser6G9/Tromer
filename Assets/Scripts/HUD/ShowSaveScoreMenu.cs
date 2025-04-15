using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSaveScoreMenu : MonoBehaviour
{
    public GameObject scoreMenu;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSaveScoreMenu()
    {
        scoreMenu.SetActive(true);
    }
}
