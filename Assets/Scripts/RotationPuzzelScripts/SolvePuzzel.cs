using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolvePuzzle : MonoBehaviour
{
    public List<RotatePiece> puzzlePieces; // Only track pieces with RotatePiece script
    public float colorDelay = 0.5f; // Delay between coloring each piece (in seconds)
    public bool HasTimer = false; // Boolean to check if the puzzle has a timer
    public float timeLimit = 60f; // Time limit in seconds for time-based puzzles
    private float timer; // Timer to track the time remaining
    private bool puzzleSolved = false; // Flag to check if puzzle is solved
    private bool puzzleFailed = false; // Flag to check if the puzzle time limit expired

    void Start()
    {
        if (HasTimer)
        {
            timer = timeLimit; // Set the initial timer
        }
    }

    void Update()
    {
        if (HasTimer && !puzzleSolved && !puzzleFailed)
        {
            // Decrease the timer if the puzzle has a timer
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                // If time runs out, reset the puzzle
                puzzleFailed = true;
                ResetPuzzle();
            }
        }

        if (!puzzleFailed)
        {
            CheckPuzzleSolved();
        }
    }

    void CheckPuzzleSolved()
    {
        foreach (RotatePiece piece in puzzlePieces)
        {
            if (!piece.IsCorrectRotation()) return; // If any piece is incorrect, stop checking
        }

        // If all pieces are correctly rotated, lock their rotation and color them one by one
        if (!puzzleSolved)
        {
            puzzleSolved = true;
            LockAllPieces();
            StartCoroutine(ColorPieces());
        }
    }

    // Lock all pieces so they can't be rotated anymore
    void LockAllPieces()
    {
        foreach (RotatePiece piece in puzzlePieces)
        {
            piece.LockRotation(); // Lock each piece's rotation
        }
    }

    // Color pieces one by one after the puzzle is solved
    IEnumerator ColorPieces()
    {
        // Loop through each child and set its color to cyan (00FFFF) in order
        foreach (Transform child in transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = new Color(0f, 1f, 1f); // Set color to cyan (00FFFF)
                yield return new WaitForSeconds(colorDelay); // Wait before coloring the next piece
            }
        }
    }

    // Reset the puzzle if time runs out
    void ResetPuzzle()
    {
        // Reset each piece to its starting rotation
        foreach (RotatePiece piece in puzzlePieces)
        {
            piece.ResetRotation(); // Assuming ResetRotation method is available in RotatePiece
        }

        // Optionally, reset the puzzle pieces' colors to the initial state
        foreach (Transform child in transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.white; // Reset to original color (or any other default color)
            }
        }

        // Reset timer for the next attempt
        if (HasTimer)
        {
            timer = timeLimit;
        }

        puzzleSolved = false; // Allow puzzle to be solved again
        puzzleFailed = false; // Reset the failure flag
    }
}
