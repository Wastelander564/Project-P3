using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Properly check the tag
        {
            CoinBank.Instance.AddCoin(); // Notify CoinBank to increase the coin count
            Destroy(gameObject); // Destroy the coin after collection
        }
    }
}
