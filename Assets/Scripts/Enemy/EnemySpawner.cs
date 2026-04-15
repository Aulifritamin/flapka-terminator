using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private Transform _pointsContainer;
    [SerializeField] private Transform[] _spawnPoints;

    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: GettingFromPool,
            actionOnRelease: ReleasingCleanUp,
            actionOnDestroy: enemy => Destroy(enemy.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
    }

    private void Start()
    {
        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            yield return wait;
            Enemy enemy = _enemyPool.Get();
        }
    }

    private void GettingFromPool(Enemy enemy)
    {
        Transform spawnPoint = GetRandomPosition();
        enemy.OnDespawn += _enemyPool.Release;
        enemy.SetPosition(spawnPoint);
        enemy.gameObject.SetActive(true);
    }

    private void ReleasingCleanUp(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.OnDespawn -= _enemyPool.Release;
    }

    private Transform GetRandomPosition()
    {
        int minIndex = 0;
        int maxIndex = _spawnPoints.Length;

        int randomIndex = Random.Range(minIndex, maxIndex);
        Transform spawnPoint = _spawnPoints[randomIndex];

        return spawnPoint;
    }

    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = _pointsContainer.childCount;
        _spawnPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _spawnPoints[i] = _pointsContainer.GetChild(i);
    }
}
