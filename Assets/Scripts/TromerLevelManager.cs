using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TromerLevelManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public int coins = 0; // Cada vez que el dron recoja una coin, el HUD sumará +1 al contador

    public void UpdateCoinsText()
    {
        coinsText.text = coins.ToString();
    }

    private void Update()
    {
        UpdateCoinsText();
    }
}
