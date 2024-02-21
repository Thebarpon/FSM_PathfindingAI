using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] public Transform m_player;
    [SerializeField] public float m_detectionDistance;
    List<CharacterState> m_possibleStates;
    CharacterState m_currentState;
    public NavMeshAgent m_agent;
    private bool m_isIdle = false;
    public Transform[] waypoints;
    public int currentWaypointIndex = 0;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void CreatePossibleStates()
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new RoamingState());
        m_possibleStates.Add(new IdleState());
        m_possibleStates.Add(new PursuitState());
    }

    void Start()
    {
        CreatePossibleStates();

        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }

        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();

    }

    private void Update()
    {
        m_currentState.OnUpdate();
        TryStateTransition();
    }


    private void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }

    private void TryStateTransition()
    {
        if (!m_currentState.CanExit())
        {
            return;
        }

        //Je PEUX quitter le state actuel
        foreach (var state in m_possibleStates)
        {
            if (m_currentState.Equals(state))
            {
                continue;
            }

            if (state.CanEnter(m_currentState))
            {
                //Quitter le state actuel
                m_currentState.OnExit();
                m_currentState = state;
                //Rentrer dans le state state
                m_currentState.OnEnter();
                return;
            }
        }
    }

    public CharacterState GetCurrentState()
    {
        return m_currentState;
    }

    public bool PlayerIsNear()
    {

        RaycastHit hit;

        Vector3 direction = m_player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, m_detectionDistance))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.DrawRay(transform.position, direction, Color.green);
                //Debug.Log("Did Hit");
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, direction, Color.red);
                //Debug.Log("Did not Hit");
                return false;
            }

        }
        return false;
    }

    public NavMeshAgent GetAgent()
    {
        return m_agent;
    }

    public Transform GetPlayerTransform()
    {
        return m_player;
    }

    public void SetIdle(bool isIdle)
    {
        m_isIdle = isIdle;
    }

    public bool IsIdle() { return m_isIdle; }
}



