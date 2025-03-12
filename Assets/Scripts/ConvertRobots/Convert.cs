using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convert : MonoBehaviour
{
    private static int ConvertedRobots = 0; // Static variable must be initialized
    private bool GoodEnding = false;
    public GameObject Indicator;

    void Update()
    {
        if (ConvertedRobots == 12)
        {
            GoodEnding = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RobotSlave") // Correct tag comparison
        {
            Indicator.SetActive(true); // Correct SetActive usage
            
            if (Input.GetKeyDown(KeyCode.E)) // Correct input detection
            {
                ConvertedRobots += 1;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Indicator.SetActive(false); // Correct SetActive usage
    }
}
