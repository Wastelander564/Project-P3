using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class CoinBank : MonoBehaviour
{
    private float coinAmount = 0; // Tracks the number of collected coins
    [SerializeField] private TextMeshProUGUI counter; // UI Text for displaying the coin count

    private void Start()
    {
        coinAmount = PlayerPrefs.GetFloat("coinAmount");
        UpdateCounter(); // Ensure UI is initialized correctly
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddCoin(100000);
        }
    }

    public void AddCoin(float worth)
    {
        coinAmount += worth; // Increase coin count by the coin's worth
        PlayerPrefs.SetFloat("coinAmount", coinAmount);
        UpdateCounter(); // Update the counter UI
    }

    public float CoinAmount()
    {
        return coinAmount;
    }

    private void UpdateCounter()
    {
        counter.text = $"{coinAmount}"; // Format as 000000000
    }
}
