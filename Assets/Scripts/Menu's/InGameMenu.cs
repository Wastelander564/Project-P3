using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//I am put on the Canvas
public class InGameMenu : MonoBehaviour
{
    public GameObject settings;

    // Update is called once per frame
    private void Start()
    {
        if (settings == null) Debug.Log("no settings screen");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Settings();
        }
    }

    public void Settings()
    {
        settings.gameObject.SetActive(!settings.gameObject.activeSelf);
        if (settings.gameObject.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
