using UnityEngine;
using UnityEngine.UIElements;


public class RoamingState : CharacterState
{
    

    public override void OnEnter()
    {
       
        Debug.Log("Enter state: FreeState\n");


    }

    public override void OnUpdate()
    {
        if (m_stateMachine.waypoints.Length == 0) return; // Return if no waypoints are set

        // Check if NPC has reached the current waypoint
        if (Vector3.Distance(m_stateMachine.GetAgent().transform.position, m_stateMachine.waypoints[m_stateMachine.currentWaypointIndex].position) < 1f)
        {
            m_stateMachine.currentWaypointIndex = (m_stateMachine.currentWaypointIndex + 1) % m_stateMachine.waypoints.Length; // Move to the next waypoint
        }
        else
        {
            m_stateMachine.GetAgent().SetDestination(m_stateMachine.waypoints[m_stateMachine.currentWaypointIndex].position); // Move towards the current waypoint
        }

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
       
    }

    public override bool CanEnter(IState currentState)
    {

        return !m_stateMachine.PlayerIsNear();

    }

    public override bool CanExit()
    {

        return m_stateMachine.PlayerIsNear();
    }

}