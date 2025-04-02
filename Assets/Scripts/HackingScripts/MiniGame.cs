using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    protected int lives = 3;
    [SerializeField]
    protected Detector detector;
    public bool canPlay;

    protected void Update()
    {
        if(detector == null) detector = GameManager.Instance.Player().GetComponent<Detector>();

        if (lives <= 0)
        {
            GameOver();
        }
    }
    public void LoseLife()
    {
        lives--;
        Debug.Log(lives);
    }

    private void GameOver()
    {
        Debug.Log("Ik werk");
        gameObject.SetActive(false);
        GameManager.Instance.Player().SetActive(true);
        detector.detectionValue = detector.slider.maxValue;
        Debug.Log("Ik niet");
    }

    public virtual void ResetGame()
    {
        lives = 3;
    }

    protected virtual void Win()
    {
        GameManager.Instance.Player().SetActive(true);
        GameObject.FindGameObjectWithTag("CentralHub").GetComponent<CentralHub>().isDown = true;
        GameManager.Instance.hacked = true;
        gameObject.SetActive(false);
    }
}
