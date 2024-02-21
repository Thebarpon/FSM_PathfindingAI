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
        if (m_stateMachine.waypoints.Length == 0)
        {
            Debug.Log("No waypoints set");
            return; // Return if no waypoints are set
        }
        // Check if NPC has reached the current waypoint
        if (Vector3.Distance(m_stateMachine.transform.position, m_stateMachine.waypoints[m_stateMachine.currentWaypointIndex].position) < 1f)
        {
            Debug.Log("Set destination");
            m_stateMachine.currentWaypointIndex = (m_stateMachine.currentWaypointIndex + 1) % m_stateMachine.waypoints.Length; // Move to the next waypoint
        }
        else
        {
            //Debug.Log(m_stateMachine.GetAgent());
            //Debug.Log(m_stateMachine.waypoints[m_stateMachine.currentWaypointIndex]);
            //Debug.Log(m_stateMachine.waypoints[m_stateMachine.currentWaypointIndex].position);
            m_stateMachine.GetAgent().SetDestination(m_stateMachine.waypoints[m_stateMachine.currentWaypointIndex].position); // Move towards the current waypoint
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