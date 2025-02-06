using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GridSystem gridSystem; // Keeps the grid system for movement and positioning

    // The map width and height remain for clamping purposes
    public float mapWidth = 10f;
    public float mapHeight = 10f;

    private void Start()
    {
        gridSystem = FindObjectOfType<GridSystem>(); // Get the reference to the grid system
    }

    private void Update()
    {
        Move(); // Handles player movement
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // Keep the player inside the map boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, 0f, mapWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, 0f, mapHeight);

        transform.position = newPosition;
    }

    private void OnDrawGizmos()
    {
        // Draw the map boundaries as a wireframe rectangle
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f); 
        Gizmos.DrawWireCube(new Vector3(mapWidth / 2f, mapHeight / 2f, 0), new Vector3(mapWidth, mapHeight, 0));
        
        // Draw the player position as a green sphere
        Gizmos.color = Color.green;  
        Gizmos.DrawSphere(transform.position, 0.3f); 
    }
}
