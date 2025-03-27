using UnityEngine;
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
    public GameObject vaultPromptUI;

    private Rigidbody2D rb;

    // Camera follow variables
    public Transform cameraTransform; // The camera's transform
    public float followSpeed = 5f; // How quickly the camera follows the player

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        moveSpeed = normalSpeed;
        vaultPromptUI.SetActive(false);
    }

    // Camera follow method
    void FollowCamera()
    {
        if (cameraTransform == null) return;

        // Follow the player's position smoothly with the camera
        Vector3 targetPosition = new Vector3(rb.position.x, rb.position.y, cameraTransform.position.z);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime * followSpeed);
    }

    private void Update()
    {

        if (!isVaulting)
        {
            Move();
        }

        if (!isVaulting)
        {
            HandleSpeedModifiers();
        }

        if (Input.GetKeyDown(KeyCode.V) && vaultableObject != null && !isVaulting)
        {
            StartCoroutine(VaultOverObject(vaultableObject));
        }
    }

    private void FixedUpdate()
    {
       FollowCamera();
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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX == 0 && moveY == 0)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }

    IEnumerator VaultOverObject(Transform vaultable)
    {
        isVaulting = true;
        vaultPromptUI.SetActive(false);

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

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vaultable"))
        {
            vaultableObject = other.transform;
            vaultPromptUI.SetActive(true);
        }
        else if (other.CompareTag("SlowZone"))
        {
            SetMoveSpeed(slowSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Vaultable"))
        {
            vaultableObject = null;
            vaultPromptUI.SetActive(false);
        }
        else if (other.CompareTag("SlowZone"))
        {
            SetMoveSpeed(normalSpeed);
        }
    }
}
