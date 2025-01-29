using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptiTask1 : MonoBehaviour
{
    private TromerLevelManager levelManager;
    private void OnEnable()
    {
        levelManager = GameObject.FindObjectOfType<TromerLevelManager>();
    }
    
    // OptiTask 1: (pulsar todos los botones)
    public bool taskComplete = false;
    public List<Button> taskButtons;
    public int taskButtonsActivedCount = 0;
    public float oxigenPercentageToIncrement = 25;
    

    // Update is called once per frame
    void Update()
    {
        if (taskComplete)
        {
            levelManager.optiTask1HUDText.SetActive(true);
        }
        else
        {
            levelManager.optiTask1HUDText.SetActive(false);
        }
    }
   
    public void OptiTask1State(bool state)
    {
        taskComplete = state;
        
        if (taskComplete)
        {
            levelManager.oxigenProgressTime += (oxigenPercentageToIncrement * levelManager.totalOxigenTime) / 100;
        }
        else
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
        
        /*else
        {
            taskButtons[button - 1].GetComponent<Image>().color = new Color(1f, 0.1784818f, 0.06132078f, 1f);
            taskButtonsActivedCount--;
        }*/
    }
}
