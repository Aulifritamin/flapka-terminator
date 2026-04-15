using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootPosition;

    private WaitForSeconds _wait = new WaitForSeconds(1f);

    private ObjectPool<Bullet> _bulletPool;
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _bulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: GettingFromPool,
            actionOnRelease: ReleasingCleanUp,
            actionOnDestroy: bullet => Destroy(bullet.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
            );
    }

    private void OnEnable()
    {
        _attackCoroutine = StartCoroutine(AttackRoutine());
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.OnDespawn += _bulletPool.Release;

        return bullet;
    }

    private void GettingFromPool(Bullet bullet)
    {
        bullet.transform.position = _shootPosition.position;
        bullet.gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
        bullet.gameObject.SetActive(true);
        bullet.InitDirection(Vector2.left);
    }

    private void ReleasingCleanUp(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private IEnumerator AttackRoutine()
    {
        while (enabled)
        {
            yield return _wait;
            _bulletPool.Get();
        }
    }
}
