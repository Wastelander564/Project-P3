using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from arrow keys or WASD
        moveInput.x = Input.GetAxisRaw("Horizontal"); // Left/Right movement
        moveInput.y = Input.GetAxisRaw("Vertical");   // Up/Down movement
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }
}
