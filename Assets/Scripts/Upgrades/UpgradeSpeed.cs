using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpeed : UpgradesMain
{
    private PlayerController pm;
    protected override void Start()
    {
        upgradeName = "Speed";
        base.Start();
        pm = GameManager.Instance.Player().GetComponent<PlayerController>();
        UpdateUI();
        CheckBreakdownValue();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        CheckBreakdownValue();
    }

    protected override void Reset()
    {
        base.Reset();
        CheckBreakdownValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    void CheckBreakdownValue()
    {
        PlayerPrefs.SetFloat("speed", level);
    }
}
