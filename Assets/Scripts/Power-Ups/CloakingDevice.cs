using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloakingDevice : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invisibility cloak = other.GetComponent<Invisibility>();

            if (cloak != null)
            {
                cloak.ActivateInvisibility();
                Destroy(gameObject); // Fixed Destroy syntax
            }
        }
    }
}
