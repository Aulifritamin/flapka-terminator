using UnityEngine;

public class Ground : MonoBehaviour
{
    private int _groundDamage = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(_groundDamage);
        }
    }
}
