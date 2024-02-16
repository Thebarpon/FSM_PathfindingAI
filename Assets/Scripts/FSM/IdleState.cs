using UnityEngine;


public class IdleState : CharacterState
{


    public override void OnEnter()
    {

        Debug.Log("Enter state: FreeState\n");
    }

    public override void OnUpdate()
    {

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
