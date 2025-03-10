using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolvePuzzle : MonoBehaviour
{
    public List<RotatePiece> puzzlePieces; // Only track pieces with RotatePiece script
    public float colorDelay = 0.5f; // Delay between coloring each piece (in seconds)

    void Update()
    {
        CheckPuzzleSolved();
    }

    void CheckPuzzleSolved()
    {
        foreach (RotatePiece piece in puzzlePieces)
        {
            if (!piece.IsCorrectRotation()) return; // If any piece is incorrect, stop checking
        }

        // If all pieces are correctly rotated, color them one by one
        StartCoroutine(ColorPieces());
    }

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
}
