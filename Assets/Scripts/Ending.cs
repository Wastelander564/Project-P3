using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject ending1, ending2;
    void Start()
    {
        if (GameManager.Instance.Karma() > 0) ending2.SetActive(true);
        else ending1.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
