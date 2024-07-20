using System;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private const string WayPointTag = "WayPoint";

    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private float _speed;

    private int _currentWayPointIndex;

    public event Action<Target> OnTargetMove;

    private void Start()
    {
        _currentWayPointIndex = 1;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_currentWayPointIndex].position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation((_wayPoints[_currentWayPointIndex].position - transform.position).normalized);

        OnTargetMove?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == WayPointTag)
        {
            SetNextWayPoint();
        }
    }

    private void SetNextWayPoint()
    {
        if (_currentWayPointIndex + 1 == _wayPoints.Count)
            _currentWayPointIndex = 0;
        else
            _currentWayPointIndex++;
    }
}
