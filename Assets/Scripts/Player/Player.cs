using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Health _health;

    public event Action OnDie;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.OnDeath += Die;
    }

    private void OnDisable()
    {
        _health.OnDeath -= Die;
    }

    private void Die()
    {
        OnDie?.Invoke();
    }
}
