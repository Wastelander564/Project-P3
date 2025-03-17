using System.Collections;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float speedIncrease = 3f;
    public float boostDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                StartCoroutine(ApplySpeedBoost(player));
                gameObject.GetComponent<Collider2D>().enabled = false; // Disable collider to prevent re-triggering
                gameObject.GetComponent<SpriteRenderer>().enabled = false; // Hide the object
            }
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerController player)
    {
        float originalSpeed = player.normalSpeed;
        float originalRunSpeed = player.runSpeed;

        player.SetMoveSpeed(originalSpeed + speedIncrease);
        player.runSpeed += speedIncrease;
        
        yield return new WaitForSeconds(boostDuration);
        
        player.SetMoveSpeed(originalSpeed);
        player.runSpeed = originalRunSpeed;
        
        Destroy(gameObject); // Destroy after boost duration
    }
}
