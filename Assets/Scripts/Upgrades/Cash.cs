using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cash : MonoBehaviour
{
    //private CashScript cashScript;
    private TextMeshProUGUI text;
    private int test = 1;

    private void Start()
    {
        //cashScript = 
        text = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(text.text != test.ToString())text.text = PlayerPrefs.GetFloat("coinAmount").ToString();
    }
}
