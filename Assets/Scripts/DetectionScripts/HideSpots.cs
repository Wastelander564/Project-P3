using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpots : MonoBehaviour
{
    [SerializeField]
    private bool isHiding;

    // Update is called once per frame
    void Update()
    {
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Detector>().isHiding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Detector>().isHiding = false;
        }
    }
}
