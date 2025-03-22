using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : Settings
{
    private AudioSource audioSource;

    protected override void Start()
    {
        keyWord = "Volume";
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) return;
        base.Start();

        // Direct het volume instellen bij start
        UpdateVolume(valueNumber);

        // Luisteren naar slider veranderingen
        slider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float value)
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().volume = Mathf.Clamp01(value); // Volume altijd tussen 0 en 1
        }
    }
}
