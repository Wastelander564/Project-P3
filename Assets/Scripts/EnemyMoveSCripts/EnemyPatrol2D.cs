using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol2D : MonoBehaviour
{
    public Transform[] waypoints; // Set waypoints in the Inspector
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent is 3D by default, so constrain movement to X-Y for 2D
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Move to the next waypoint when close enough
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
}