using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform[] _waypoints;

    private const float _reachThreshold = 0.01f;
    private int _currentWaypointIndex = 0;

    private void Start()
    {
        if (_waypoints == null || _waypoints.Length == 0)
        {
            InitializeWaypoints();
        }
    }

    private void Update()
    {
        if (_waypoints.Length == 0) return;

        MoveToCurrentWaypoint();
    }

    private void InitializeWaypoints()
    {
        _waypoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _waypoints[i] = transform.GetChild(i);
        }
    }

    private void MoveToCurrentWaypoint()
    {
        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _speed * Time.deltaTime);

        if (HasReachedTarget(targetWaypoint))
        {
            UpdateToNextWaypoint();
        }
    }

    private void UpdateToNextWaypoint()
    {
        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        UpdateForwardDirection();
    }

    private void UpdateForwardDirection()
    {
        Vector3 nextWaypointPosition = _waypoints[_currentWaypointIndex].position;
        transform.forward = nextWaypointPosition - transform.position;
    }

    private bool HasReachedTarget(Transform targetWaypoint)
    {
        return Vector3.Distance(transform.position, targetWaypoint.position) < _reachThreshold;
    }
}