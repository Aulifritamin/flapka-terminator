using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private InputLisiner _inputLisiner;
    [SerializeField] private float _jumpForce;

    private Vector2 _movement;
    private Vector2 _startPosition;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionX;
        _rigidbody.constraints |= RigidbodyConstraints2D.FreezeRotation;
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        _inputLisiner.JumpPressed += Jump;
    }

    private void OnDisable()
    {
        _inputLisiner.JumpPressed -= Jump;
    }

    private void Jump()
    {
        _rigidbody.linearVelocityY = 0;

        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
