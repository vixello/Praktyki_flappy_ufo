using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    public float flapForce = 5f;

    [SerializeField]
    private GameObject player;
    private PlayerInput playerInput;

    private InputAction touchTapAction;
    Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchTapAction = playerInput.actions.FindAction("TouchTap");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
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
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, flapForce);
    }
}
