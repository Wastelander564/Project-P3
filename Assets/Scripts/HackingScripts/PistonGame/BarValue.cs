using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarValue : MiniGame
{
    public Slider answerSlider;
    public float answerMaxValue = 100f;
    public float answerValue;

    public Slider playerBar;

    void Start()
    {
        // Zorg ervoor dat de slider een referentie heeft en de juiste max waarde krijgt
        if (answerSlider != null)
        {
            answerSlider.maxValue = answerMaxValue;
            answerSlider.value = answerValue;
        }
        if(playerBar != null)
        {
            playerBar.maxValue = answerMaxValue;
        }
    }

    private new void Update()
    {
        base.Update();
        if (Mathf.Abs(playerBar.value - answerSlider.value) < 1f) // Tolerantie van 1
        {
            playerBar.value = answerMaxValue;
            canPlay = false;
            Invoke("Win", 5);
        }
    }

}
