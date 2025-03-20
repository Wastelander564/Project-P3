using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Interact : MonoBehaviour
{
    private bool isClose;
    public GameObject interactButton;
    private GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player == null) player = collision.gameObject;
            isClose = true;
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isClose = false;
            interactButton.SetActive(false);
        }
    }

    public bool IsClose()
    {
        return isClose;
    }

    public GameObject Player()
    {
        return player;
    }

}
