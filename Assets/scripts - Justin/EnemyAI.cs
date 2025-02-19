using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints; // Patrol points
    public Transform player; // Player reference
    public float chaseRange = 5f; // Chase if path distance is <= 5
    public float stopDistance = 1f; // Stop moving when close to player

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        if (IsPlayerWithinNavMeshRange(chaseRange))
        {
            StartChasing();
        }
        else
        {
            Patrol();
        }
    }

    void StartChasing()
    {
        isChasing = true;
        float distance = GetNavMeshPathDistance(player.position);

        if (distance > stopDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); // Stop moving when close
        }
    }

    void Patrol()
    {
        if (isChasing)
        {
            isChasing = false;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }

        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    bool IsPlayerWithinNavMeshRange(float maxDistance)
    {
        float pathDistance = GetNavMeshPathDistance(player.position);
        return pathDistance > 0 && pathDistance <= maxDistance;
    }

    float GetNavMeshPathDistance(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(targetPosition, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            float distance = 0f;
            for (int i = 1; i < path.corners.Length; i++)
            {
                distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
            return distance;
        }
        return Mathf.Infinity; // Return Infinity if path is not possible
    }
}
