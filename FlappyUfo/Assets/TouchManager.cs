using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour
{
    public float flapForce = 5f;

    [SerializeField]
    private GameObject player;
    private PlayerInput playerInput;
    private AudioManager audioManager;

    private InputAction touchTapAction;
    Rigidbody2D playerRigidbody;
    private AudioSource jumpAudioSource;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
/*        jumpAudioSource = audioManager.transform.Find("sfx").GetComponent<AudioSource>();
*/
        playerInput = GetComponent<PlayerInput>();
        touchTapAction = playerInput.actions.FindAction("TouchPress");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        touchTapAction.performed += TouchPress;
    }

    private void OnDisable()
    {
        touchTapAction.performed -= TouchPress;
    }

    private void TouchPress(InputAction.CallbackContext context)
    {

        if (!player.GetComponent<Player>().IsPlayerDead)
        {
            audioManager.PlaySoundEffects(audioManager.jumpSound);
        }
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, flapForce);

    }


}
