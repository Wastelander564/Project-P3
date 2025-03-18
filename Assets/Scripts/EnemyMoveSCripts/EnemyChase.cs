using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public float stopDistance = 1f;
    private NavMeshAgent agent;
    public Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ensure NavMeshAgent is configured for 2D movement
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > stopDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath(); // Stop moving when within stop distance
            }
        }
    }
}