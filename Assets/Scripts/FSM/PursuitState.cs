using UnityEngine;


public class PursuitState: CharacterState
{


    public override void OnEnter()
    {
        m_stateMachine.GetAgent().isStopped = false;
        Debug.Log("Enter state: FreeState\n");
    }

    public override void OnUpdate()
    {
        m_stateMachine.GetAgent().SetDestination(m_stateMachine.GetPlayerTransform().position);
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        m_stateMachine.GetAgent().isStopped = true;
    }

    public override bool CanEnter(IState currentState)
    {
	
        return m_stateMachine.PlayerIsNear();

    }

    public override bool CanExit()
    {
        
        return !m_stateMachine.PlayerIsNear();
    }

}
