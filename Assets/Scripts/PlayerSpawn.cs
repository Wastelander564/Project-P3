using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = transform.position;
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag 'Player'!");
        }
    }
}
