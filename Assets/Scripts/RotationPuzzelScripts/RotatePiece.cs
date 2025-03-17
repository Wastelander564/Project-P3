using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePiece : MonoBehaviour
{
    public List<float> validRotations; // List of valid rotations for this piece
    public KeyCode rotateKey = KeyCode.R; // Customizable key in Inspector
    private bool isInsideTrigger = false;
    private bool isLocked = false; // Flag to lock rotation after the puzzle is solved
    private float startingRotation; // Store the starting rotation

    void Start()
    {
        // Store the initial rotation of the piece when the game starts
        startingRotation = transform.eulerAngles.z;
    }

    // Trigger detection for the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = false;
        }
    }

    void Update()
    {
        if (isLocked)
        {
            return; // Prevent any updates if the piece is locked
        }

        // If the player is inside the trigger area and presses the rotate key, rotate the object
        if (isInsideTrigger && Input.GetKeyDown(rotateKey))
        {
            RotateObject();
        }
    }

    // Rotate the object 90 degrees at a time
    void RotateObject()
    {
        float newRotation = transform.eulerAngles.z + 90f;
        if (newRotation > 270f)
        {
            newRotation = 0f;
        }

        // Apply the rotation to the piece
        transform.rotation = Quaternion.Euler(0, 0, newRotation);
    }

    // Check if the current rotation matches any valid rotations for this piece
    public bool IsCorrectRotation()
    {
        float rotation = transform.eulerAngles.z;
        foreach (float validRotation in validRotations)
        {
            if (Mathf.Approximately(rotation, validRotation))
            {
                return true;
            }
        }
        return false;
    }

    // Lock the rotation of the piece
    public void LockRotation()
    {
        isLocked = true; // Set the lock flag to true
    }

    // Reset to the starting rotation
    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, startingRotation); // Reset to the starting rotation
    }
}
