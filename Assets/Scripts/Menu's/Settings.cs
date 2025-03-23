using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    protected float valueNumber;
    protected Slider slider;
    protected string keyWord;
    private float defaultNumber;


    protected virtual void Start()
    {
        slider = GetComponentInChildren<Slider>();
        if (slider == null) return;
        defaultNumber = (slider.maxValue + slider.minValue) / 2f;
        valueNumber = GetPlayerPref(keyWord);
        slider.value = valueNumber;
        slider.onValueChanged.AddListener(value => SetThePlayerPref(keyWord, value));
    }

    protected void SetThePlayerPref(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save(); // Opslaan naar schijf
    }

    protected float GetPlayerPref(string key, float defaultValue = -1)
    {
        if (defaultValue == -1) defaultValue = defaultNumber;
        return PlayerPrefs.GetFloat(key, defaultValue);
    }
}
