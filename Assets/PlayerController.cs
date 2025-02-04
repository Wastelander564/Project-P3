using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Image detectionBar;
    public GridSystem gridSystem;
    public float visibilityLevel = 0f;

    // ik heb de map height/width op de player gezet zodat we zelf in de inspecotr gwn kunnen kiezen hoe groot de map word heel snel 
    public float mapWidth = 10f;
    public float mapHeight = 10f;

    private void Start()
    {
        gridSystem = FindObjectOfType<GridSystem>();
    }

    private void Update()
    {
        Move();
        UpdateVisibility();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

       
        newPosition.x = Mathf.Clamp(newPosition.x, 0f, mapWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, 0f, mapHeight);

        transform.position = newPosition;
    }

    void UpdateVisibility()
    {
        visibilityLevel = gridSystem.GetLightingLevel(transform.position);
        detectionBar.fillAmount = visibilityLevel; // hallo hallo hallo
    }

 
    private void OnDrawGizmos()
    {

        Gizmos.color = new Color(1f, 0f, 0f, 0.2f); 
        
        
        Gizmos.DrawWireCube(new Vector3(mapWidth / 2f, mapHeight / 2f, 0), new Vector3(mapWidth, mapHeight, 0));
        
      
        Gizmos.color = Color.green;  
        Gizmos.DrawSphere(transform.position, 0.3f); 
    }
}
