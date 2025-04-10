using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject player;
    public bool hacked;

    private int karma;
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
    public int Karma()
    {
        return karma;
    }

    public void KarmaUp()
    {
        karma++;
    }
    public GameObject Player()
    {
        return player;
    }
}
