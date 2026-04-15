using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Health _health;

    private string _respawnTag = "Respawn";
    private float _speed = 0.8f;

    private Rigidbody2D _rigidBody;

    public event Action <Enemy> OnDespawn;

    public void SetPosition(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _health.OnDeath += Die;
        _rigidBody.linearVelocity = Vector2.left * _speed;
    }

    private void OnDisable()
    {
        _health.OnDeath -= Die;
    }

    private void Die()
    {
        OnDespawn.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_respawnTag))
        {
            OnDespawn.Invoke(this);
        }
    }
}

