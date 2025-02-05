using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [SerializeField] private float _speedForward;
    [SerializeField] private float _speedBackward;

    [SerializeField] private float _clampPosition;

    private bool m_isSwitchDirection = false;

    private void Update()
    {
        transform.Translate(Vector3.forward * (!m_isSwitchDirection ? _speedForward : -_speedBackward) * Time.deltaTime);

        if (transform.position.z >= _clampPosition)
        {
            m_isSwitchDirection = true;
        }
        else if (transform.position.z <= -_clampPosition)
        {
            m_isSwitchDirection = false;
        }
    }
}
