using UnityEngine;
using System.Collections;
public class CrushingWalls : MonoBehaviour
{
    [System.Serializable]
    public class Wall
    {
        public GameObject wallObject;  // The wall to move
        public float moveAmount;       // How far the wall should move (positive or negative)
        public float moveSpeed = 2f;   // Speed of movement
        public Vector3 moveDirection;  // Direction of movement (left, right, up, down)
    }

    public Wall[] walls; // Array of walls to move
    private bool triggered = false; // Ensure the walls move only once

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true; // Prevent further triggering
            MoveWalls();     // Start the wall movement
        }
    }

    private void MoveWalls()
    {
        foreach (var wall in walls)
        {
            // Move each wall based on the direction
            StartCoroutine(MoveWall(wall));
        }
    }

    private IEnumerator MoveWall(Wall wall)
    {
        Vector3 startPos = wall.wallObject.transform.position; // Store the initial position of the wall
        Vector3 targetPos = startPos + wall.moveDirection * wall.moveAmount; // Calculate target position

        float elapsedTime = 0f;

        // Move the wall over time
        while (elapsedTime < 1f)
        {
            wall.wallObject.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime); // Lerp movement
            elapsedTime += Time.deltaTime * wall.moveSpeed; // Increment the time
            yield return null; // Wait for the next frame
        }

        wall.wallObject.transform.position = targetPos; // Ensure the wall is at the target position
    }

}
