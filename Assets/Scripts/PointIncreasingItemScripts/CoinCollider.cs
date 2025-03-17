using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    public int Worth; // The worth of this coin

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Properly check the tag
        {
            CoinBank.Instance.AddCoin(Worth); // Pass the coin's worth to CoinBank
            Destroy(gameObject); // Destroy the coin after collection
        }
    }
}
