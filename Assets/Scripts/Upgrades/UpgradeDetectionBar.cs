using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UpgradeDetectionBar : UpgradesMain
{

    private Detector detector;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        upgradeName = "Detection";
        detector = GameManager.Instance.Player().GetComponent<Detector>();

        level = PlayerPrefs.GetInt($"{upgradeName}" + "Level", level);
        cost = PlayerPrefs.GetFloat($"{upgradeName}" + "Cost", 100f * Mathf.Pow(2, level));

        UpdateUI();
        CheckDetectionValue();
    }

    public override void Upgrade()
    {
        base.Upgrade();

        /*if(cost > currentMoney)
        {
            return;
        }*/
        PlayerPrefs.SetInt($"{upgradeName}" + "Level", level);
        PlayerPrefs.SetFloat($"{upgradeName}" + "Cost", cost);
        PlayerPrefs.Save(); // Direct opslaan

        CheckDetectionValue();

    }

    protected override void Reset()
    {
        base.Reset();
        PlayerPrefs.SetInt($"{upgradeName}" + "Level", 0);
        PlayerPrefs.SetFloat($"{upgradeName}" + "Cost", 100);
        PlayerPrefs.Save(); // Direct opslaan
        base.UpdateUI();

        CheckDetectionValue();

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    void CheckDetectionValue()
    {
        detector.slider.maxValue = 100 + level * 25;
        detector.slider.transform.localScale = new Vector3 (3 + 0.5f * level, 3, 3);
    }
}
