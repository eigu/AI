using UnityEngine;
using Unity.AI.Navigation;
using NUnit.Framework.Constraints;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _meshSurface;

    [SerializeField] private GameObject _obstaclePrefab;

    [SerializeField] private int _spawnAmount;
    [SerializeField] private Vector2 _spawnPosition;
    [SerializeField] private Vector2 _spawnSize;

    [SerializeField] private LayerMask _floorMask;

    private void Start()
    {
        for (int i = 0; i < _spawnAmount;)
        {
            float xPosition = Random.Range(_spawnPosition.x, _spawnPosition.y);
            float zPosition = Random.Range(_spawnPosition.x, _spawnPosition.y);

            Vector3 newPosition = new Vector3(xPosition, transform.position.y, zPosition);

            float xSize = Random.Range(_spawnSize.x, _spawnSize.y);
            float zSize = Random.Range(_spawnSize.x, _spawnSize.y);

            Vector3 newSize = new Vector3(xSize, 1f, zSize);

            Ray ray = new Ray(newPosition, Vector3.down);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, _floorMask))
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, SphereRadius(xSize, zSize));

                if (colliders.Length == 1)
                {
                    Vector3 newObstaclePosition = new Vector3(hit.point.x, 0.5f, hit.point.z);

                    GameObject newObstacle = Instantiate(_obstaclePrefab, newObstaclePosition, Quaternion.identity);

                    newObstacle.transform.localScale = new Vector3(xSize, 1, zSize);

                    i++;
                }
            }
        }

        _meshSurface.BuildNavMesh();
    }

    private float SphereRadius(float xSize, float zSize)
    {
        if (xSize > zSize) return xSize;

        return zSize;
    }
}
