using UnityEngine;


public class RoamingState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("Enter state: Roaming\n");
        m_stateMachine.GetAgent().isStopped = false;
    }

    public override void OnUpdate()
    {
        // Check if there are no waypoints set for roaming
        if (m_stateMachine.m_waypoints.Length == 0)
        {
            Debug.Log("No waypoints set");
            return; // Exit the method early if there are no waypoints
        }
        // Check if the character has reached its current waypoint
        if (Vector3.Distance(m_stateMachine.transform.position, m_stateMachine.m_waypoints[m_stateMachine.m_currentWaypointIndex].position) < 1f)
        {
            Debug.Log("Reached waypoint, setting next destination");
            m_stateMachine.m_currentWaypointIndex = (m_stateMachine.m_currentWaypointIndex + 1) % m_stateMachine.m_waypoints.Length; // Move to the next waypoint
        }
        else
        {
            // Sets the character's destination to the current waypoint's position
            m_stateMachine.GetAgent().SetDestination(m_stateMachine.m_waypoints[m_stateMachine.m_currentWaypointIndex].position); // Move towards the current waypoint
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("Exit roaming state");
    }

    public override bool CanEnter(IState currentState)
    {

        return !m_stateMachine.PlayerIsNear()
            && !m_stateMachine.IsIdle();

    }

    public override bool CanExit()
    {

        return m_stateMachine.PlayerIsNear();
    }
}