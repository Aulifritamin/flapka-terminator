using UnityEngine;
using UnityEngine.Pool;

public class Attack : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _firePosition;

    [SerializeField] private float _shootDelay = 0.5f;

    private float _nextShootTime = 0f;

    private InputLisiner _inputLisiner;
    private ObjectPool<Bullet> _bulletPool;

    private void Awake()
    {
        _inputLisiner = GetComponent<InputLisiner>();

        _bulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: GettingFromPool,
            actionOnRelease: ReleasingCleanUp,
            actionOnDestroy: bullet => Destroy(bullet.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 100
        );
    }

    private void OnEnable()
    {
        _inputLisiner.ShootPressed += Fire;
    }

    private void OnDisable()
    {
        _inputLisiner.ShootPressed -= Fire;
    }

    private void Fire()
    {
        if (Time.time < _nextShootTime)
        {
            return;
        }

        _bulletPool.Get();

        _nextShootTime = Time.time + _shootDelay;
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.OnDespawn += (bullet) => _bulletPool.Release(bullet);

        return bullet;
    }

    private void GettingFromPool(Bullet bullet)
    {
        bullet.transform.position = _firePosition.position;
        bullet.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
        bullet.gameObject.SetActive(true);
        bullet.InitDirection(Vector2.right);
    }

    private void ReleasingCleanUp(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
