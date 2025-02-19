using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBreakdown : MonoBehaviour
{
    public bool isDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isDown = !isDown;
        }
        if (isDown)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.5f);
            GetComponent<PlayerController>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
            GetComponent<PlayerController>().enabled = true;

        }
    }
}
