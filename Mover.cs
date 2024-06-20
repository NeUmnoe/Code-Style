using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform[] _waypoints;

    private const float _reachThreshold = 0.01f;
    private int _currentWaypointIndex = 0;
    private Transform _currentTargetWaypoint;

    private void Start()
    {
        if (_waypoints == null || _waypoints.Length == 0)
        {
            InitializeWaypoints();
        }
    }

    private void Update()
    {
        if (_waypoints.Length == 0)
        {
            return;
        }

        MoveToCurrentWaypoint();

        if (HasReachedTarget())
        {
            UpdateToNextWaypoint();
        }
    }

    private void InitializeWaypoints()
    {
        _waypoints = new Transform[transform.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i] = transform.GetChild(i);
        }
    }

    private void MoveToCurrentWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTargetWaypoint.position, _speed * Time.deltaTime);
    }

    private void UpdateToNextWaypoint()
    {
        _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;

        SetCurrentTargetWaypoint();
        UpdateForwardDirection();
    }

    private void SetCurrentTargetWaypoint()
    {
        _currentTargetWaypoint= _waypoints[_currentWaypointIndex];
    }

    private void UpdateForwardDirection()
    {
        Vector3 nextWaypointPosition = _waypoints[_currentWaypointIndex].position;
        transform.forward = nextWaypointPosition - transform.position;
    }

    private bool HasReachedTarget()
    {
        return Vector3.SqrMagnitude(transform.position - _currentTargetWaypoint.position) < _reachThreshold * _reachThreshold;
    }
}