using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsPlayerDead = false;
    public Animator Animator;
    public GameObject Finish;

    private void FixedUpdate()
    {
        if (IsPlayerDead)
        {
            PlayerDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ObstacleCollider")
        {
            IsPlayerDead = true;
        }
    }

    private void PlayerDeath()
    {
        Animator.SetBool("isDead", true);
        GetComponent<PlayerMovement>().StopMoving();
        Finish.GetComponent<GameSceneManager>().IsFinised = true;
    }
}
