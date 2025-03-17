using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Piston : MonoBehaviour
{
    private BarValue barvalue;
    public bool isOn;
    public float value;

    public TextMeshProUGUI OnText;
    public TextMeshProUGUI OffText;

    // Start is called before the first frame update
    void Start()
    {
        barvalue = GetComponentInParent<BarValue>();
        if (isOn) IncreaseValue(value);
        ChangeTextColor();
    }
    public void TurnOn()
    {
        if (!barvalue.canPlay) return;
        isOn = !isOn;
        ChangeTextColor();
        if (isOn)
        {
            IncreaseValue(value);
            return;
        }
        IncreaseValue(-value);
        
    }

    private void IncreaseValue(float value)
    {
        barvalue.playerBar.value += value;
    }

    private void ChangeTextColor()
    {
        OnText.color = isOn ? Color.green : Color.black;
        OffText.color = isOn ? Color.black : Color.green;
    }
}
