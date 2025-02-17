using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float runSpeed = 8f;
    public float slowSpeed = 2f;
    public float vaultSpeed = 2f;
    public float vaultDuration = 0.5f;

    private float moveSpeed;
    private bool isSlowWalking = false;
    private bool isVaulting = false;
    private Transform vaultableObject;

    public GameObject vaultPromptUI; // Assign the VaultPrompt UI Image in the Inspector

    private void Start()
    {
        moveSpeed = normalSpeed;
        vaultPromptUI.SetActive(false); // Hide at start
    }

    private void Update()
    {
        if (!isVaulting)
        {
            HandleSpeedModifiers();
            Move();
        }

        if (Input.GetKeyDown(KeyCode.V) && vaultableObject != null && !isVaulting)
        {
            StartCoroutine(VaultOverObject(vaultableObject));
        }
    }

    void HandleSpeedModifiers()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isSlowWalking)
        {
            moveSpeed = runSpeed;
        }
        else if (!isSlowWalking)
        {
            moveSpeed = normalSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isSlowWalking = !isSlowWalking;
            moveSpeed = isSlowWalking ? slowSpeed : normalSpeed;
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        transform.position = newPosition;
    }

    IEnumerator VaultOverObject(Transform vaultable)
    {
        isVaulting = true;
        vaultPromptUI.SetActive(false); // Hide prompt while vaulting

        Vector3 startPos = transform.position;
        Vector3 vaultTarget = GetVaultTargetPosition(vaultable);

        float elapsedTime = 0f;

        while (elapsedTime < vaultDuration)
        {
            transform.position = Vector3.Lerp(startPos, vaultTarget, elapsedTime / vaultDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = vaultTarget;
        isVaulting = false;
    }

    Vector3 GetVaultTargetPosition(Transform vaultable)
    {
        Collider2D vaultCollider = vaultable.GetComponent<Collider2D>();
        if (vaultCollider == null) return transform.position;

        Vector3 vaultPosition = vaultable.position;
        float objectWidth = vaultCollider.bounds.size.x;
        float objectHeight = vaultCollider.bounds.size.y;

        Vector3 targetPosition = transform.position;

        if (transform.position.x < vaultPosition.x)
        {
            targetPosition = new Vector3(vaultPosition.x + objectWidth / 2 + 0.5f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > vaultPosition.x)
        {
            targetPosition = new Vector3(vaultPosition.x - objectWidth / 2 - 0.5f, transform.position.y, transform.position.z);
        }

        if (objectHeight > objectWidth)
        {
            if (transform.position.y < vaultPosition.y)
            {
                targetPosition = new Vector3(transform.position.x, vaultPosition.y + objectHeight / 2 + 0.5f, transform.position.z);
            }
            else
            {
                targetPosition = new Vector3(transform.position.x, vaultPosition.y - objectHeight / 2 - 0.5f, transform.position.z);
            }
        }

        return targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vaultable"))
        {
            vaultableObject = other.transform;
            vaultPromptUI.SetActive(true); // Show prompt when near vaultable object
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Vaultable"))
        {
            vaultableObject = null;
            vaultPromptUI.SetActive(false); // Hide prompt when leaving
        }
    }
}
