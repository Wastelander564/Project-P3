using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class CoinBank : MonoBehaviour
{
    public static CoinBank Instance; // Singleton pattern to access from CoinCollider
    private int coinAmount = 0; // Tracks the number of collected coins
    [SerializeField] private TextMeshProUGUI counter; // UI Text for displaying the coin count

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assign instance for global access
        }
    }

    private void Start()
    {
        UpdateCounter(); // Ensure UI is initialized correctly
    }

    public void AddCoin()
    {
        coinAmount++; // Increase coin count
        UpdateCounter(); // Update the counter UI
    }

    private void UpdateCounter()
    {
        counter.text = coinAmount.ToString("D9"); // Format as 000000000
    }
}
