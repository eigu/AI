using UnityEngine;
using UnityEngine.AI;

public class AIRootMotionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maxTarget;

    private void OnValidate()
    {
        if (!_agent) _agent = GetComponent<NavMeshAgent>();
        if (!_animator) _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_agent.hasPath)
        {
            var direction = (_agent.steeringTarget - transform.position).normalized;
            var animationDirection = transform.InverseTransformDirection(direction);
            var isFacingMoveDirection = Vector3.Dot(direction, transform.forward) > .5f;

            _animator.SetFloat("Horizontal", isFacingMoveDirection ? animationDirection.x : 0, .5f, Time.deltaTime);
            _animator.SetFloat("Vertical", isFacingMoveDirection ? animationDirection.z : 0, .5f, Time.deltaTime);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 100 * Time.deltaTime);

            if (Vector3.Distance(transform.position, _agent.destination) < _agent.radius)
            {
                _agent.ResetPath();
            }
        }
        else
        {
            _animator.SetFloat("Horizontal", 0, .25f, Time.deltaTime);
            _animator.SetFloat("Vertical", 0, .25f, Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var isHit = Physics.Raycast(ray, out RaycastHit hit, _maxTarget);

            if (isHit)
            {
                _agent.destination = hit.point;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_agent.hasPath)
        {
            for (int i = 0; i < _agent.path.corners.Length; i++)
            {
                Debug.DrawLine(_agent.path.corners[i], _agent.path.corners[i +1], Color.blue);
            }
        }
    }
}
