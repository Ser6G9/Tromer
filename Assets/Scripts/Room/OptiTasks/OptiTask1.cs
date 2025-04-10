using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptiTask1 : MonoBehaviour
{
    private GameManager levelManager;
    private void OnEnable()
    {
        levelManager = GameObject.FindObjectOfType<GameManager>();
    }
    
    // OptiTask 1: (pulsar todos los botones)
    public bool taskComplete = false;
    public List<Button> taskButtons;
    public int taskButtonsActivedCount = 0;
    public float oxigenPercentageToIncrement = 25f;
    public float restoreTaskDelay = 60.0f;
    public float restoreTaskTimeProgress = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (taskComplete && restoreTaskTimeProgress < restoreTaskDelay)
        {
            levelManager.optiTask1HUDText.SetActive(true);
            restoreTaskTimeProgress += Time.deltaTime;
        } else if (restoreTaskTimeProgress >= restoreTaskDelay)
        {
            RestartTask();
        }
        
        if(!taskComplete)
        {
            levelManager.optiTask1HUDText.SetActive(false);
        }
    }

    public void ActivateButton(int button)
    {
        if (taskButtonsActivedCount < taskButtons.Count)
        {
            taskButtons[button - 1].GetComponent<Image>().color = new Color(0.66f, 1f, 0.18f, 1f);
            taskButtonsActivedCount++;
        }
        
        // Si todos los botones han sido activados se completa la tarea y se suma la cantidad de oxígeno extra.
        if (!taskComplete && taskButtonsActivedCount == taskButtons.Count)
        {
            taskComplete = true;
            levelManager.oxigenProgressTime += (oxigenPercentageToIncrement * levelManager.totalOxigenTime) / 100;
        }
    }

    public void RestartTask()
    {
        taskComplete = false;
        restoreTaskTimeProgress = 0;
        levelManager.optiTask1HUDText.SetActive(false);
        taskButtonsActivedCount = 0;
        
        for (int i = 0; i < taskButtons.Count; i++)
        {
            taskButtons[i].GetComponent<Image>().color = new Color(1f, 0.1784818f, 0.06132078f, 1f);
        }
        
    }
}
