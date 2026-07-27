using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float acceleration = 12f;
    public float deceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 14f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheck;
    public float wallCheckRadius = 0.2f;
    public LayerMask wallLayer;
    public float wallSlideSpeed = -2f;
    public float wallJumpForce = 14f;
    public float wallJumpPush = 10f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;

    private float coyoteCounter;
    private float jumpBufferCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        Debug.Log(isGrounded);

        // Wall check
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);

        // Coyote time
        if (isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        // Jump buffering
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        // Wall slide logic
        isWallSliding = false;
        if (!isGrounded && isTouchingWall && moveInput != 0)
        {
            isWallSliding = true;
        }

        // Jump
        if (jumpBufferCounter > 0)
        {
            if (coyoteCounter > 0) // normal jump
            {
                Jump();
            }
            else if (isWallSliding) // wall jump
            {
                WallJump();
            }
        }
            if (rb.linearVelocity.y <= 0.01f)
    {
        isGrounded = Physics2D.BoxCast(groundCheck.position, new Vector2(0.45f, 0.05f), 0f, Vector2.down, 0.02f, groundLayer).collider != null;
    }
    else
    {
        isGrounded = false;
    }
    }

    void FixedUpdate()
    {
        // Wall slide vertical control
        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, wallSlideSpeed);
            return; // stop normal movement while sliding
        }

        // Smooth movement
        float targetSpeed = moveInput * moveSpeed;
        float speedDiff = targetSpeed - rb.linearVelocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        float movement = speedDiff * accelRate;

        rb.AddForce(new Vector2(movement, 0));
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    
        jumpBufferCounter = 0f;
        coyoteCounter = 0f; 
        isGrounded = false; 
    }

    void WallJump()
    {
        float pushDir = -Mathf.Sign(moveInput); 
        rb.linearVelocity = new Vector2(pushDir * wallJumpPush, wallJumpForce);

        jumpBufferCounter = 0f;
        coyoteCounter = 0f; 
        isGrounded = false;
    }


    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        if (wallCheck != null)
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
    }
}
