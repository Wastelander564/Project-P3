using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesMain : MonoBehaviour
{
    [SerializeField]
    protected int level;
    [SerializeField]
    protected float cost;  // start value

    protected string upgradeName; // Unique name for each upgrade. PlayerPrefs use this for their values
    public GameObject levels; // the blocks that indicate the levels
    public Button button; // click here for the upgrade

    [SerializeField]
    protected MessageLog messageLog; // box with feedback text messages

    private CoinBank coinBank;


    protected virtual void Start()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();
        messageLog = GetComponentInParent<MessageLog>();
        coinBank = GameManager.Instance.Player().GetComponentInChildren<CoinBank>();
        Debug.Log(coinBank);
        level = PlayerPrefs.GetInt($"{upgradeName}" + "Level", level);
        cost = PlayerPrefs.GetFloat($"{upgradeName}" + "Cost", 100f * Mathf.Pow(2, level));
    }

    private void OnEnable()
    {
        messageLog.DeleteMessages();
    }

    public virtual void Upgrade()
    {
        if (PlayerPrefs.GetInt($"{upgradeName}" + "Level") == 4)
        {
            messageLog.AddCraftingMessage("All Max level", "#00FF00");
            return;
        }
        if (cost > coinBank.CoinAmount()) return;
        coinBank.AddCoin(-cost);
        level++;
        PlayerPrefs.SetInt($"{upgradeName}" + "Level", level);
        PlayerPrefs.SetFloat($"{upgradeName}" + "Cost", cost);
        PlayerPrefs.Save(); // Direct opslaan
        messageLog.AddCraftingMessage("Upgrade succesfull", "#00FF00");
        IncreaseCost();
        UpdateUI();
    }

    protected virtual void Reset()
    {
        messageLog.AddCraftingMessage("Reset succesfull", "#00FF00");
        level = 0;
        cost = 100;
        PlayerPrefs.SetInt($"{upgradeName}" + "Level", 0);
        PlayerPrefs.SetFloat($"{upgradeName}" + "Cost", 100);
        PlayerPrefs.Save(); // Direct opslaan
        UpdateUI();
    }


    protected void IncreaseCost()
    {
        cost = Mathf.Round(cost * 2);
    }

    protected void UpdateUI()
    {
        foreach (Transform child in levels.transform)
        {
            UnityEngine.UI.Image img = child.GetComponent<UnityEngine.UI.Image>();
            if (img != null)
            {
                img.color = Color.white; // Reset alle naar wit
            }
        }
        for (int i = 0; i < level && i < levels.transform.childCount; i++)
        {
            Transform child = levels.transform.GetChild(i);

            UnityEngine.UI.Image img = child.GetComponent<UnityEngine.UI.Image>();
            if (img != null)
            {
                img.color = Color.green; // Correcte manier om de kleur te wijzigen
            }
            else
            {
            }
        }

        button.GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();
        if (level == 4) button.GetComponentInChildren<TextMeshProUGUI>().text = "MAX level";
    }
    
    public int GetLevel() => level;
    public float GetCost() => cost;
}
