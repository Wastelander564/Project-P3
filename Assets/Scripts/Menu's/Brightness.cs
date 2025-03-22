using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : Settings
{
    public Image brightness;

    protected override void Start()
    {
        keyWord = "Brightness";
        if (brightness == null) return;
        base.Start();
        // Direct de alpha instellen bij start
        UpdateBrightness(valueNumber);
        // Listener aanpassen om UpdateBrightness aan te roepen
        slider.onValueChanged.AddListener(UpdateBrightness);
    }

    private void UpdateBrightness(float value)
    {
        if (brightness != null)
        {
            Color color = brightness.color;
            color.a = Mathf.Clamp01(1-value); // Zorg ervoor dat alpha tussen 0 en 1 blijft
            brightness.color = color;
        }
        else
        {
            Debug.LogError("Brightness Image is niet ingesteld in " + gameObject.name);
        }
    }
}
