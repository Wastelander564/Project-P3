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
    public bool isHiding;
    public float incValue;

    public int enemyRank;


    private void Start()
    {
        slider.maxValue = 100 + PlayerPrefs.GetInt("DetectorLevel") * 25;
        slider.transform.localScale = new Vector3(3 + 0.5f * PlayerPrefs.GetInt("DetectorLevel"), 3, 3);
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
        }
        if (isSeen)
        {
            if (detectionValue < slider.maxValue) IncreaseDetection(30, enemyRank);
        }
        else
        {
            if(detectionValue > 0) IncreaseDetection(-10, 0);

        }
    }


    private void IncreaseDetection(float x, float rank)
    {
        float multiplier = 1;
        //if(rank <= GetComponent<FakeBreakdown>().abilityRank) //this line will be used after i make the upgrade system for the fake breakdown ability
            if (GetComponent<FakeBreakdown>().isDown && x >= 0) multiplier /= 3f;
            if (isHiding && x >= 0) multiplier -= 0.5f;
        detectionValue += (x * multiplier) * Time.deltaTime;
    }
}
