using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    protected int lives = 3;
    [SerializeField]
    protected Detector detector;
    public bool canPlay;

    private void Update()
    {
        if(detector == null) detector = YouriGameManager.Instance.Player().GetComponent<Detector>();

        if (lives <= 0)
        {
            GameOver();
        }
    }
    public void LoseLife()
    {
        lives--;
    }

    private void GameOver()
    {
        gameObject.SetActive(false);
        YouriGameManager.Instance.Player().SetActive(true);
        detector.detectionValue = detector.slider.maxValue;
    }

    public virtual void ResetGame()
    {
        lives = 3;
    }

    protected virtual void Win()
    {
        YouriGameManager.Instance.Player().SetActive(true);
        GameObject.FindGameObjectWithTag("CentralHub").GetComponent<CentralHub>().isDown = true;
        gameObject.SetActive(false);
    }
}
