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
        base.Start();
        fakeBr = YouriGameManager.Instance.Player().GetComponent<FakeBreakdown>();

        upgradeName = "BreakDown";
        // Laad eerder opgeslagen waarden (standaardwaarde is 1 en 100)
        level = PlayerPrefs.GetInt($"{upgradeName}" + "Level", level);
        cost = PlayerPrefs.GetFloat($"{upgradeName}" + "Cost", 100f * Mathf.Pow(2, level));

        UpdateUI();
        CheckBreakdownValue();
    }

    public override void Upgrade()
    {
        base.Upgrade();

        // Sla de nieuwe waarden op
        PlayerPrefs.SetInt($"{upgradeName}" + "Level", level);
        PlayerPrefs.SetFloat($"{upgradeName}" + "Cost", cost);
        PlayerPrefs.Save(); // Direct opslaan
        CheckBreakdownValue();
    }

    protected override void Reset()
    {
        base.Reset();
        PlayerPrefs.SetInt($"{upgradeName}" + "Level", 0);
        PlayerPrefs.SetFloat($"{upgradeName}" + "Cost", 100);
        PlayerPrefs.Save(); // Direct opslaan
        base.UpdateUI();
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
        //fakeBr.limit = 5 + level * 2;
    }
}
