using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorMenuAnimation : MonoBehaviour
{
    public bool doorOpen = false;
    public List<GameObject> eventList;
    public float countDawnTime;
    public bool eventAllowed = true;
    public bool closeSoundEffectOn = false;
    public AudioSource closeSoundEffect;
    public bool openSoundEffectOn = false;
    public AudioSource openSoundEffect;

    void Start()
    {
        transform.position = new Vector3(0.0f, 1.75f, 5f);
        countDawnTime = Random.Range(1.2f, 3f);
        EliminateEnvent();
    }

    void Update()
    {
        if (countDawnTime > 0)
        {
            countDawnTime -= Time.deltaTime;
        }

        if (countDawnTime <= 0)
        {
            if (doorOpen)
            {
                CloseDoor();
            }
            
            if (!doorOpen)
            {
                OpenDoor();
                if (eventAllowed)
                {
                    CreateEvent();
                    eventAllowed = false;
                }
            }
        }
        
    }

    public void OpenDoor()
    {
        Vector3 doorOpenPosition = new Vector3(0.0f, 4.25f, 5f);
        transform.position = Vector3.MoveTowards(transform.position, doorOpenPosition, 2f * Time.deltaTime);

        if (Vector3.Distance(transform.position, doorOpenPosition) < 2.4f)
        {
            if (!openSoundEffectOn)
            {
                openSoundEffect.Play();
                openSoundEffectOn = true;
            }
        }

        if (Vector3.Distance(transform.position, doorOpenPosition) < 0.01f)
        {
            transform.position = doorOpenPosition;
            doorOpen = true;
            countDawnTime = Random.Range(3.2f, 5f);
            
            openSoundEffectOn = false;
            openSoundEffect.Stop();
        }
    }
    public void CloseDoor()
    {
        Vector3 doorOpenPosition = new Vector3(0.0f, 1.75f, 5f);
        transform.position = Vector3.MoveTowards(transform.position, doorOpenPosition, 5f * Time.deltaTime);

        if (Vector3.Distance(transform.position, doorOpenPosition) < 1.8f)
        {
            if (!closeSoundEffectOn)
            {
                closeSoundEffect.Play();
                closeSoundEffectOn = true;
            }
        }
        
        if (Vector3.Distance(transform.position, doorOpenPosition) < 0.01f)
        {
            transform.position = doorOpenPosition;
            EliminateEnvent();
            doorOpen = false;
            countDawnTime = Random.Range(1f, 3f);
            
            closeSoundEffectOn = false;
        }
    }

    public void CreateEvent()
    {
        EliminateEnvent();
        eventList[Random.Range(0, eventList.Count)].SetActive(true);
    }
    
    public void EliminateEnvent()
    {
        foreach (GameObject e in eventList)
        {
            e.SetActive(false);
        }
        
        eventAllowed = true;
    }
}
