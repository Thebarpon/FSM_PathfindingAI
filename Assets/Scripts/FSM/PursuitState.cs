using UnityEngine;


public class PursuitState : CharacterState
{
    private float m_chasingMaxDuration = 5.0f;
    private float m_chasingCurrentDuration;
    public override void OnEnter()
    {
        m_stateMachine.GetAgent().isStopped = false;
        m_chasingCurrentDuration = m_chasingMaxDuration;
        Debug.Log("Enter state: Pursuit\n");
    }

    public override void OnUpdate()
    {
        // Decrease chasing duration over time, stopping the pursuit when the time runs out.
        if (m_chasingCurrentDuration > 0)
        {
            m_chasingCurrentDuration -= Time.deltaTime;
        }
        else
        {
            m_stateMachine.SetIdle(true);
        }

        // Continuously update the destination to the player's position for pursuit.
        m_stateMachine.GetAgent().SetDestination(m_stateMachine.GetPlayerTransform().position);
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        m_stateMachine.GetAgent().isStopped = true;
        Debug.Log("Exit pursuit state");
    }

    public override bool CanEnter(IState currentState)
    {

        return m_stateMachine.PlayerIsNear();

    }

    public override bool CanExit()
    {
        return !m_stateMachine.PlayerIsNear() || m_stateMachine.IsIdle();
    }
}
