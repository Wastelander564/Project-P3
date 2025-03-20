using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CentralHub : MonoBehaviour
{
    public GameObject miniGame;

    private Interact interact;

    private Detector detector;
    public bool isDown;


    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponentInParent<Interact>();
        detector = GameObject.FindAnyObjectByType<Detector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(miniGame != null)
        {
            if (interact.IsClose() && Input.GetKeyDown(KeyCode.E) && miniGame.activeSelf == false && detector.detectionValue <= detector.slider.maxValue/2 && !isDown)
            {
                miniGame.SetActive(true);
                if(miniGame.name != "Upgrades")miniGame.GetComponent<MiniGame>().ResetGame();
                if (miniGame.name != "Upgrades") miniGame.GetComponent<MiniGame>().canPlay = true;
                HaltMovement();
            }
        }
    }

    private void HaltMovement()
    {
        if(interact != null)interact.Player().SetActive(false);
    }
}
