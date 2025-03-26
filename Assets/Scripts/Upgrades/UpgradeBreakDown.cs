using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UpgradeBreakDown : UpgradesMain
{
    [SerializeField]
    private FakeBreakdown fakeBr;
    // Start is called before the first frame update
    protected override void Start()
    {
        upgradeName = "BreakDown";
        base.Start();
        fakeBr = GameManager.Instance.Player().GetComponent<FakeBreakdown>();
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
        PlayerPrefs.SetFloat("breakDownLimit",3 + level);
    }
}
