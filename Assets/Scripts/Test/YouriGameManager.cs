using UnityEngine;

public class YouriGameManager : MonoBehaviour
{
    public static YouriGameManager Instance;
    private GameObject player;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Zorgt ervoor dat het object niet vernietigd wordt bij scene wissels
        }
        else
        {
            Destroy(gameObject); // Voorkomt dat er meerdere instanties ontstaan
        }
    }

    private void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
    }

    // Hier kun je je game-logica toevoegen
    void Update()
    {
        
    }

    public GameObject Player()
    {
        return player;
    }
}
