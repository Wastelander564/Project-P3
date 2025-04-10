using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUnlock : MonoBehaviour
{
    private bool done;
    private bool close;

    public GameObject interactButton;

    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!done && close && Input.GetKeyDown(KeyCode.E))
        {
            canvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Unlock()
    {
        Dash.unlocked = true;
        Time.timeScale = 1;
        canvas.SetActive(false);
        GameManager.Instance.Player().GetComponent<Dash>().enabled = true;
    }

    public void IncreaseKarma()
    {
        GameManager.Instance.KarmaUp();
        Time.timeScale = 1;
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactButton.SetActive(true);
            close = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactButton.SetActive(false);
            close = false;
        }
    }
}
