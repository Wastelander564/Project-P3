using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBreakdown : MonoBehaviour
{
    private Animator Animator;
    public bool isDown;
    private Rigidbody2D rb;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // When Q is pressed, play the animation faster and set the state
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.velocity = Vector2.zero;
            playerController.enabled = false;

            Animator.SetBool("isDown", true);
            Animator.SetBool("isntDown", false);
        }
        // When Q is released, reset the animation and return speed to normal
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            playerController.enabled = true;

            Animator.SetBool("isntDown", true);
            Animator.SetBool("isDown", false);
        }
    }
}
