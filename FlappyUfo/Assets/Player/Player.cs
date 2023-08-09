using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    bool flap;
    public float moveSpeed = 5f;
    public float gravityForce = 9.81f; // Adjust this value as needed
    public Animator Animator;
    private Rigidbody2D rb;
    public GameObject Finish;


    private void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        float angleInRadians = Mathf.Deg2Rad * 30f;
        float moveX = Mathf.Cos(angleInRadians);
        float moveY = Mathf.Sin(angleInRadians);
        rb.velocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
    }
    
    private void FixedUpdate()
    {
/*
        float angleInRadians = Mathf.Deg2Rad * 30f;
        float moveX = Mathf.Cos(angleInRadians);
        float moveY = Mathf.Sin(angleInRadians);
        // Calculate movement direction based on input
        Vector2 movementDirection = new Vector2(moveX, moveY).normalized;

        rb.velocity = movementDirection * moveSpeed * Time.deltaTime;
*/

        // Apply gravity in the x-direction
/*        rb.AddForce(new Vector2(gravityForce, 0f), ForceMode2D.Force);
*/
        /*        rb.velocity += new Vector2(0f, gravityForce * Time.deltaTime);
        */
/*        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                rb.velocity = new Vector2(rb.velocity.x, flapForce);
                Debug.Log("touch");
            }
            if (touch.phase == TouchPhase.Moved)
            {

            }
            if (touch.phase == TouchPhase.Ended)
            {

            }
        }*/
        float newYVelocity = rb.velocity.y - gravityForce * Time.deltaTime;
        rb.velocity = new Vector2(rb.velocity.x, newYVelocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ObstacleCollider")
        {
            Animator.SetBool("isDead", true);
            rb.velocity = Vector3.zero;
            moveSpeed = 0f; 
            gravityForce = 0f;
            Finish.GetComponent<Finish>().IsFinised = true;
        }
    }
}
