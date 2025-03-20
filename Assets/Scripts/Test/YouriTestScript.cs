using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouriTestScript : MonoBehaviour
{
    public float timer = 4;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            transform.Rotate(0, 0, 90);
            timer = 1;
            if (gameObject.transform.rotation.z == 360)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
