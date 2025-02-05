using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    private NavMeshAgent m_agent;

    [SerializeField] private Camera _camera;

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 location = new Vector3(hit.point.x, 0 , hit.point.z);

                m_agent.SetDestination(location);
            }
        }
    }
}
