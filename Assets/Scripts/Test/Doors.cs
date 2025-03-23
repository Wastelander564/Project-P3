using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.hacked) Destroy(gameObject);
    }
}
