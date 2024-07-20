using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private Target _target;
    [SerializeField] private int _defaultPoolCapacity = 40;
    [SerializeField] private int _maxPoolSize = 100;

    private ObjectPool<Enemy> _zombiesPool;

    private void OnEnable()
    {
        _timer.TimeChanged += _zombiesPool.Get;
    }

    private void OnDisable()
    {
        _timer.TimeChanged -= _zombiesPool.Get;
    }

    private void Awake()
    {
        _zombiesPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab, _spawnPosition, Quaternion.identity),
            actionOnGet: (enemy) => Spawn(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: false,
            defaultCapacity: _defaultPoolCapacity,
            maxSize: _maxPoolSize
            );
    }

    private void Spawn(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = _spawnPosition;
        enemy.Init(_target);
    }

    public void ReleaseEnemy(Enemy enemy) => _zombiesPool.Release(enemy);

}
