using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    private Target _target;
    private Vector3 _targetPosition;

    private void OnDisable()
    {
        _target.OnTargetMove -= OnTargetPositionChanged;
    }

    private void Update()
    {
        if(_targetPosition != Vector3.zero)
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

        transform.LookAt(_targetPosition);
    }

    public void Init(Target target)
    {
        SetTarget(target);
        _target.OnTargetMove += OnTargetPositionChanged;
    }

    private void SetTarget(Target target)
    {
        _target = target;
        _targetPosition = target.transform.position;
    }

    private void OnTargetPositionChanged(Target target)
    { 
        SetTarget(target);
    }
}
