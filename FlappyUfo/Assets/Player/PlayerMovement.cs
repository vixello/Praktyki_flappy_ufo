using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float GravityForce = 9.81f; // Adjust this value as needed
    private Rigidbody2D rb;

    private void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        MoveWithAngle(30f);
    }

    private void FixedUpdate()
    {
        float newYVelocity = rb.velocity.y - GravityForce * Time.deltaTime;
        rb.velocity = new Vector2(rb.velocity.x, newYVelocity);
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
        MoveSpeed = 0f;
        GravityForce = 0f;
    }

    private void MoveWithAngle(float angle)
    {
        float angleInRadians = Mathf.Deg2Rad * angle;
        float moveX = Mathf.Cos(angleInRadians);
        float moveY = Mathf.Sin(angleInRadians);
        rb.velocity = new Vector2(moveX * MoveSpeed, moveY * MoveSpeed);
    }
}
