using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    private int _damage = 1;
    private float _speed = 2f;

    private Vector2 _direction;
    private Rigidbody2D _rigidBody;
    private Coroutine _lifeTimer;
    private WaitForSeconds _wait = new WaitForSeconds(5f);

    public event Action<Bullet> OnDespawn;

    public void InitDirection(Vector2 direction)
    {
        _direction = direction;
        _rigidBody.WakeUp();
        _rigidBody.linearVelocity = _direction * _speed;
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;
    }

    private void OnEnable()
    {
        _lifeTimer = StartCoroutine(TimeToLife());
    }

    private void OnDisable()
    {
        if (_lifeTimer != null)
        {
            StopCoroutine(_lifeTimer);
        }
        _rigidBody.linearVelocity = Vector2.zero;
        _rigidBody.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
        
        Despawn();
    }

    private IEnumerator TimeToLife()
    {
        yield return _wait;
        Despawn();
    }

    private void Despawn()
    {
        OnDespawn?.Invoke(this);
    }
}
