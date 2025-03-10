using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public bool Invisible = false;
    private Detector detector;
    private bool isTimerRunning = false; // Prevents coroutine from restarting

    void Start()
    {
        detector = GetComponent<Detector>();
    }

    void Update()
    {
        if (detector != null && Invisible)
        {
            detector.isSeen = false; // Prevent detection from increasing
            detector.detectionValue = Mathf.Max(0, detector.detectionValue - (10 * Time.deltaTime)); // Slowly decrease detection
        }
    }

    public void ActivateInvisibility()
    {
        if (!Invisible)
        {
            Invisible = true;
            if (!isTimerRunning)
            {
                StartCoroutine(TurnOff());
            }
        }
    }

    IEnumerator TurnOff()
    {
        isTimerRunning = true;
        yield return new WaitForSeconds(5);
        Invisible = false;
        isTimerRunning = false;
    }
}
