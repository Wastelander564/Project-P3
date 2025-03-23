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
    protected float cost;  // Beginwaarde
    protected string upgradeName;
    public GameObject levels;
    public Button button;
    [SerializeField]
    protected MessageLog messageLog;


    protected virtual void Start()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();
        messageLog = GetComponentInParent<MessageLog>();
    }

    private void OnEnable()
    {
        messageLog.DeleteMessages();
    }

    public virtual void Upgrade()
    {
        level++;
        messageLog.AddCraftingMessage("Upgrade succesfull", "#00FF00");
        IncreaseCost();
        UpdateUI();
    }

    protected virtual void Reset()
    {
        messageLog.AddCraftingMessage("Reset succesfull", "#00FF00");
        level = 0;
        cost = 100;
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
