using UnityEngine;

public class MapChunkLoader : MonoBehaviour
{
    private Transform playerCamera;
    public float loadDistance = 15f;
    private GameObject[] mapChunks;

    void Start()
    {
        playerCamera = Camera.main.transform;
        mapChunks = new GameObject[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            mapChunks[i] = transform.GetChild(i).gameObject;
            mapChunks[i].SetActive(false);
        }
    }

    void Update()
    {
        foreach (GameObject chunk in mapChunks)
        {
            float distance = Vector3.Distance(playerCamera.position, chunk.transform.position);

            if (distance < loadDistance && !chunk.activeSelf)
            {
                chunk.SetActive(true);
            }
            else if (distance >= loadDistance && chunk.activeSelf)
            {
                chunk.SetActive(false);
            }
        }
    }
}
