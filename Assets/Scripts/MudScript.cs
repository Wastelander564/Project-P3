using UnityEngine;

public class MudScript : MonoBehaviour
{
    public float mudSpeed = 1f; // Speed when in mud
    public float mudSprintSpeed = 2f; // Sprint speed in mud
    public GameObject mudEffectUI; // Assign a UI image in the Inspector

    private PlayerController playerController;
    private float originalNormalSpeed;
    private float originalRunSpeed;

    private void Start()
    {
        if (mudEffectUI != null)
        {
            mudEffectUI.SetActive(false); // Hide mud effect at start
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Save original speeds
                originalNormalSpeed = playerController.normalSpeed;
                originalRunSpeed = playerController.runSpeed;

                // Reduce speeds in mud
                playerController.normalSpeed = mudSpeed;
                playerController.runSpeed = mudSprintSpeed;
                playerController.SetMoveSpeed(mudSpeed); // Apply slowed speed

                if (mudEffectUI != null)
                {
                    mudEffectUI.SetActive(true); // Show mud effect
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerController != null)
        {
            // Restore original speeds
            playerController.normalSpeed = originalNormalSpeed;
            playerController.runSpeed = originalRunSpeed;
            playerController.SetMoveSpeed(originalNormalSpeed); // Apply restored speed

            if (mudEffectUI != null)
            {
                mudEffectUI.SetActive(false); // Hide mud effect
            }
        }
    }
}
