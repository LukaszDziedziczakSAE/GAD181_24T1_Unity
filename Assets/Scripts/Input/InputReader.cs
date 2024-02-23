using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    [SerializeField] float raycastDistance;
    [SerializeField] LayerMask raycastLayers;
    [field: SerializeField, Header("DEBUG")] public Vector2 TouchPosition { get; private set; }
    [field: SerializeField] public bool TouchPressed { get; private set; }

    public event Action OnTouchPressed;
    public event Action OnTouchReleased;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
        //TouchSimulation.Enable();
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

    public void OnMousePress(InputAction.CallbackContext context)
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
            //TouchPosition = Vector2.zero;
            TouchPressed = false;
            OnTouchReleased?.Invoke();
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        TouchPosition = context.ReadValue<Vector2>();
    }

    /*public void OnTouchPress(InputAction.CallbackContext context)
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
    }*/

    public RaycastHit RaycastFromTouchPoint
    {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(TouchPosition);
            //Debug.Log("TouchPosition " + TouchPosition);
            //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 50, Color.red, 5f);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, raycastLayers))
            {
                //Debug.Log("HitPoint " + hit.point);
                return hit;
            }
            else
            {
                //Debug.LogWarning("No Hit");
                return new RaycastHit();
            }

            
        }
    }
}
