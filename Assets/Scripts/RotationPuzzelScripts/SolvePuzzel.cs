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
    private bool timerActive = false; // Each puzzle has its own timer status

    void Start()
    {
        if (HasTimer)
        {
            timer = timeLimit; // Set the initial timer for this puzzle
            timerActive = false; // Timer does NOT start automatically
        }
    }

    void Update()
    {
        if (HasTimer && timerActive && !puzzleSolved && !puzzleFailed)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
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
            if (!piece.IsCorrectRotation()) return;
        }

        if (!puzzleSolved)
        {
            puzzleSolved = true;
            timerActive = false; // Stop the timer once the puzzle is solved
            LockAllPieces();
            StartCoroutine(ColorPieces());
        }
    }

    void LockAllPieces()
    {
        foreach (RotatePiece piece in puzzlePieces)
        {
            piece.LockRotation();
        }
    }

    IEnumerator ColorPieces()
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = new Color(0f, 1f, 1f); // Set color to cyan (00FFFF)
                yield return new WaitForSeconds(colorDelay);
            }
        }
    }

    void ResetPuzzle()
    {
        foreach (RotatePiece piece in puzzlePieces)
        {
            piece.ResetRotation(); // Assuming ResetRotation() exists in RotatePiece
        }

        foreach (Transform child in transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.white;
            }
        }

        if (HasTimer)
        {
            timer = timeLimit;
            timerActive = false; // Ensure the timer does not restart automatically
        }

        puzzleSolved = false;
        puzzleFailed = false;
    }

    // Call this method to activate the timer for this specific puzzle
    public void StartTimer()
    {
        if (HasTimer && !timerActive)
        {
            timerActive = true;
        }
    }

    public void StopTimer()
    {
        if (HasTimer && timerActive)
        {
            timerActive = false;
        }
    }
}
