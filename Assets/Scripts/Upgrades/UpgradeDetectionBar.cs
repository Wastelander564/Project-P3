using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class UpgradeDetectionBar : UpgradesMain
{

    private Detector detector;

    // Start is called before the first frame update
    protected override void Start()
    {
        upgradeName = "Detection";
        base.Start();
        detector = GameManager.Instance.Player().GetComponent<Detector>();
        UpdateUI();
        CheckDetectionValue();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        CheckDetectionValue();

    }

    protected override void Reset()
    {
        base.Reset();
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
        PlayerPrefs.SetInt("DetectorLevel", level);
        detector.slider.maxValue = 100 + PlayerPrefs.GetInt("DetectorLevel") * 25;
        detector.slider.transform.localScale = new Vector3(3 + 0.5f * PlayerPrefs.GetInt("DetectorLevel"), 3, 3);
    }
}
