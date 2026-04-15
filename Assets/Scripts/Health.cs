using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _health = 1;

    public event Action OnDeath;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
