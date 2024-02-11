using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 TouchPosition { get; private set; }
    public bool TouchPressed { get; private set; }

    public event Action OnTouchPressed;
    public event Action OnTouchReleased;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
        TouchSimulation.Enable();
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TouchPosition = context.ReadValue<Vector2>();

            if (!TouchPressed)
            {
                OnTouchPressed?.Invoke();
                TouchPressed = true;
            }
        }

        else if (context.canceled)
        {
            TouchPosition = Vector2.zero;
            TouchPressed = false;
            OnTouchReleased?.Invoke();
        }
    }

    public void OnTouchPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!TouchPressed)
            {
                OnTouchPressed?.Invoke();
                TouchPressed = true;
            }
        }

        else if (context.canceled)
        {
            TouchPosition = Vector2.zero;
            TouchPressed = false;
            OnTouchReleased?.Invoke();
        }
    }
}
