using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;

    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private enum State { Idle, Patrol, Chase, Attack }
    private State currentState = State.Idle;

    void Start()
    {
        currentState = State.Patrol; // Set the initial state to Patrol
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Patrol:
                PatrolUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
        }
    }

    private bool isWaiting = false; // Add this flag

    private void IdleUpdate()
    {
        if (!isWaiting)
        {
            isWaiting = true; // Prevent the coroutine from being started multiple times
            StartCoroutine(IdleWaitThenPatrol());
        }
        // Optionally, handle other logic that should occur during idle state
    }

    private void PatrolUpdate()
    {
        if (waypoints.Length == 0) return; // Return if no waypoints are set

        // Check if NPC has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Move to the next waypoint
        }
        else
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position); // Move towards the current waypoint
        }

        // Transition to Chase if player is detected
        if (Vector3.Distance(player.position, transform.position) <= detectionRange)
        {
            currentState = State.Chase;
        }
    }

    private void ChaseUpdate()
    {
        isWaiting = false;

        if (Vector3.Distance(player.position, transform.position) > detectionRange)
        {
            currentState = State.Idle; // Lost sight of player, return to Idle
        }
        else if (Vector3.Distance(player.position, transform.position) <= attackRange)
        {
            currentState = State.Attack; // Close enough to attack
        }
        else
        {
            agent.SetDestination(player.position); // Move towards the player
        }
    }

    private bool isAttacking = false;

    private void AttackUpdate()
    {
        if (!isAttacking)
        {
            isAttacking = true; // Prevents re-entry until attack cycle is complete
            PushPlayer();
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        currentState = State.Idle;
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        agent.isStopped = false; // Re-enable the NavMeshAgent movement
        currentState = State.Patrol; // Return to patrol state
        isAttacking = false; // Reset flag for next attack
    }

    private void PushPlayer()
    {
        agent.isStopped = true; // Stop the NavMeshAgent from moving the NPC
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float pushForce = 5f;
            playerRb.AddForce(direction * pushForce, ForceMode.Impulse);
        }
    }

    IEnumerator IdleWaitThenPatrol()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds in idle state
        currentState = State.Patrol; // Transition back to patrol state
        isAttacking = false; // If this flag is only for attacks, reset it here or as appropriate
    }
}