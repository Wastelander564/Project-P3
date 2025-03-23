using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints; // Patrol points
    public float stopDistance = 1f; // Stop moving when close to player
    public float detectionThreshold = 90f; // Detection level to start chasing
    public float patrolThreshold = 30f; // Detection level to return to patrolling
    public Transform visionCone; // Reference to the vision cone (if you have one)

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool isChasing = false;
    private Detector detector;
    
    public int Rank;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;  // We'll handle rotation manually
        agent.updateUpAxis = false;
        detector = FindObjectOfType<Detector>(); // Find the player’s Detector component

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        if (GameManager.Instance.hacked)
        {
            if (GetComponentInChildren<MeshRenderer>().enabled)
            {
                GetComponentInChildren<VisionCone>().enabled = false;
                GetComponentInChildren<PolygonCollider2D>().enabled = false;
            }
            return;
        }
        if (detector.detectionValue >= detectionThreshold)
        {
            StartChasing();
        }
        else if (detector.detectionValue <= patrolThreshold)
        {
            Patrol();
        }

        RotateTowardsDestination();
    }

    void StartChasing()
    {
        isChasing = true;
        float distance = Vector3.Distance(transform.position, detector.transform.position);

        if (distance > stopDistance)
        {
            agent.SetDestination(detector.transform.position);
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
        if (waypoints.Length == 0) return; // Prevent division by zero when no waypoints exist

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    void RotateTowardsDestination()
    {
        if (agent.hasPath)
        {
            // Get the direction of movement (2D rotation is based on X and Y, Z for rotation)
            Vector3 direction = agent.steeringTarget - transform.position;

            // Only rotate if the agent is moving
            if (direction.sqrMagnitude > 0.01f)
            {
                // Calculate the angle for 2D rotation (Z-axis)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Smoothly rotate to the target angle
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 5f);

                // If you have a vision cone, rotate it to match the enemy's direction with a 90-degree offset
                if (visionCone != null)
                {
                    // Add a 90-degree offset to the vision cone's rotation
                    visionCone.rotation = Quaternion.Slerp(visionCone.rotation, Quaternion.Euler(0, 0, angle - 90f), Time.deltaTime * 5f);
                }
            }
        }
    }
}
