using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputLisiner : MonoBehaviour
{
    private const Key _jump = Key.Space;
    private const Key _shoot = Key.V;

    public event Action JumpPressed;
    public event Action ShootPressed;

    private void Update()
    {
        bool isJumpPressed = Keyboard.current[_jump].wasPressedThisFrame;

        if (isJumpPressed)
        {
            JumpPressed?.Invoke();
        }

        bool isShootPressed = Keyboard.current[_shoot].wasPressedThisFrame;

        if (isShootPressed)
        {
            ShootPressed?.Invoke();
        }
    }
}
