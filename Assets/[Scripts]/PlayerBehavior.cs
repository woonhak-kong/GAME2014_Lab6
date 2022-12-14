using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Player Movement")]
    public float horizontalForce;
    public float horizontalSpeed;
    public float verticalForce;
    public float airFactor;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;
        Move();
        Jump();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (x != 0.0f)
            x = x > 0.0f ? 1.0f : -1.0f;
        rigidbody.AddForce(Vector2.right * x * horizontalForce * (isGrounded ? 1 : airFactor));
        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, horizontalSpeed);
    }

    private void Jump()
    {
        float y = Input.GetAxis("Jump");
        if (isGrounded && y > 0.0f)
        {
            rigidbody.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }
}
