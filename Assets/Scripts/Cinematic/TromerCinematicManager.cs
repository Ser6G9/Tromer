using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cinematic
{
    public class TromerCinematicManager : MonoBehaviour
    {
        public GameObject camera1;
        public GameObject camera2;
        public GameObject camera3;

        public float time = 0.0f;
        UIEventHandlers uiEvent = GameObject.FindObjectOfType<UIEventHandlers>();
        
        // Start is called before the first frame update
        void Start()
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
            camera3.SetActive(false);
        }
    
        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if(time >= 3.5f)
            {
                camera1.SetActive(false);
                camera2.SetActive(true);
            }
            else if(time >= 6.5f)
            {
                camera2.SetActive(false);
                camera3.SetActive(true);
            }
            else if(time >= 7.5f)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}

