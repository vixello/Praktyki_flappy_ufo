using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction touchTapAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchTapAction = playerInput.actions.FindAction("TouchTap");
    }

    private void OnEnable()
    {
        touchTapAction.performed += TouchTap;
    }

    private void OnDisable()
    {
        touchTapAction.performed -= TouchTap;
    }

    private void TouchTap(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log("value: " + value);
    }
}
