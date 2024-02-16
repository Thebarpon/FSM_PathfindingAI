using UnityEngine;
using UnityEngine.AI;

public class NavmeshController : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;
    [SerializeField]
    private NavMeshAgent m_agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                m_agent.SetDestination(hit.point);
            }
        }
    }
}
