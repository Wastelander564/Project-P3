using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Detector : MonoBehaviour
{
    public bool isSeen;
    public float detectionValue = 0;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == 0)
        {
            slider.fillRect.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        else
        {
            slider.fillRect.GetComponent<Image>().color = Color.white; // Of een andere gewenste kleur
        }
        if (slider != null)
        {
            slider.value = detectionValue;
            UpdateUI();
        }
        if (isSeen)
        {
            if (detectionValue < 100) IncreaseDetection(30);
        }
        else
        {
            if(detectionValue > 0) IncreaseDetection(-10);

        }
    }


    private void IncreaseDetection(float x)
    {
        float multiplier = 1;
        if (GetComponent<FakeBreakdown>().isDown) multiplier = 0.1f;
        detectionValue += (x * multiplier) * Time.deltaTime;
    }

    private void UpdateUI()
    {

    }
}
