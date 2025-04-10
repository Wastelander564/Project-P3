using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public static bool unlocked = false;
    private bool isDashing = false;
    private Vector2 dashDirection;
    private Rigidbody2D rb;

    public float dashSpeed;
    public float dashCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (unlocked == false) this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dashCooldown > -1)
        {
            dashCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldown <= 0)
        {
            dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (dashDirection == Vector2.zero) dashDirection = Vector2.right;
            isDashing = true;
            DisableCollision();
            rb.velocity = Vector2.zero;
            rb.AddForce(dashDirection * dashSpeed * GetComponent<Transform>().localScale.x, ForceMode2D.Impulse);
            dashCooldown = 3;
            Invoke("Reset", 0.3f);
        }
    }

    void DisableCollision()
    {
        // Zet de interactie tussen PlayerDash en Pitfall tijdelijk uit
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Pitfall"), true);
    }

    void EnableCollision()
    {
        // Zet de interactie weer aan
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Pitfall"), false);
    }


    private void Reset()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        isDashing = false;
        EnableCollision();
    }

    public bool IsDashing() => isDashing;
}
