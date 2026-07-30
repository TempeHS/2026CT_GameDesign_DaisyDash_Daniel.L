using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 11f;          
    public float groundAcceleration = 14f; 
    public float groundDeceleration = 13f; 
    public float airAcceleration = 9f;    
    public float airDeceleration = 4f;     

    [Header("Jumping")]
    public float jumpForce = 15f;
    public float jumpCutMultiplier = 0.5f; 
    public float coyoteTime = 0.10f;       
    public float jumpBufferTime = 0.10f;   
    public float maxFallSpeed = -22f;      

    [Header("Bunny Hopping")]
    public float bhopSpeedMultiplier = 1.15f; 
    public float maxBhopSpeed = 22f;          

    [Header("Wall Check & Slide")]
    public float wallSlideSpeed = -1.5f; 
    public float wallJumpForceY = 16f;     
    public float wallJumpForceX = 14f;     
    public float wallJumpControlTime = 0.15f; 
    public LayerMask climbableWallLayer; 

    [Header("Climbing Settings")]
    public KeyCode climbKey = KeyCode.C;  
    public float climbSpeed = 5f;          

    [Header("Dash Settings")]
    public float dashSpeed = 28f;          
    public float dashDuration = 0.15f;     
    public float dashEndSpeedMultiplier = 0.7f; 
    public float dashResetDelay = 0.1f; 
    public KeyCode dashKey = KeyCode.LeftShift; 

    private Rigidbody2D rb;
    private float moveInputX;
    private float moveInputY;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isClimbing;               
    private float coyoteCounter;
    private float jumpBufferCounter;
    
    private bool _isGroundedNow;
    private bool _isTouchingWallNow;

    private bool canDash = true;
    private bool isDashing;
    private float originalGravity;
    private float dashResetTimestamp;
    private float wallJumpTimer;

    private bool isGrounded() => _isGroundedNow;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale; 
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if (isDashing && jumpBufferCounter > 0 && isGrounded() && coyoteCounter > 0)
        {
            StopAllCoroutines(); 
            rb.gravityScale = originalGravity; 
            isDashing = false;
            Jump();
        }

        if (isDashing) return;

        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");
        
        isTouchingWall = _isTouchingWallNow;

        if (isWallWallJumpingLockout())
        {
            wallJumpTimer -= Time.deltaTime;
        }

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;

            if (!canDash && Time.time >= dashResetTimestamp)
            {
                canDash = true;
            }
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            coyoteCounter = 0f;
        }
 
        isClimbing = isTouchingWall && Input.GetKey(climbKey);

        isWallSliding = false;
        if (!isGrounded() && isTouchingWall && !isClimbing && moveInputX != 0 && rb.linearVelocity.y < 0)
        {
            isWallSliding = true;
        }

        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartCoroutine(PerformDash());
        }

        if (jumpBufferCounter > 0)
        {
            if (coyoteCounter > 0) 
            {
                Jump();
            }
            else if (isWallSliding || isClimbing) 
            {
                WallJump();
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        if (isClimbing)
        {
            rb.gravityScale = 0f; 
            rb.linearVelocity = new Vector2(0f, moveInputY * climbSpeed);
            return;
        }

        rb.gravityScale = originalGravity;

        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }

        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, wallSlideSpeed);
            return; 
        }

        if (isWallWallJumpingLockout()) return;

        float targetSpeed = moveInputX * moveSpeed;
        float speedDiff = targetSpeed - rb.linearVelocity.x;
        
        float accelRate;
        if (isGrounded())
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? groundAcceleration : groundDeceleration;
        }
        else
        {
            if (Mathf.Abs(rb.linearVelocity.x) > moveSpeed && Mathf.Sign(rb.linearVelocity.x) == Mathf.Sign(moveInputX))
            {
                accelRate = 0.1f; 
            }
            else
            {
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? airAcceleration : airDeceleration;
            }
        }

        float movement = speedDiff * accelRate;
        rb.AddForce(new Vector2(movement, 0));
    }

void Jump()
{
    float currentHorizontalSpeed = rb.linearVelocity.x;

    if (isDashing && isGrounded())
    {
        float flatSpeedBoost = moveSpeed + 7f; 
        currentHorizontalSpeed = Mathf.Sign(rb.linearVelocity.x) * flatSpeedBoost;
    }
    else if (isGrounded() && jumpBufferCounter > 0)
    {
        if (moveInputX != 0 && Mathf.Abs(currentHorizontalSpeed) > 0.1f)
        {
            currentHorizontalSpeed *= bhopSpeedMultiplier;
            currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed, -maxBhopSpeed, maxBhopSpeed);
        }
    }

    rb.linearVelocity = new Vector2(currentHorizontalSpeed, jumpForce);

    _isGroundedNow = false; 
    coyoteCounter = 0f; 
    jumpBufferCounter = 0f; 
}


    void WallJump()
    {
        float pushDir = -Mathf.Sign(moveInputX); 
        
        if (moveInputX == 0)
        {
            pushDir = transform.localScale.x > 0 ? -1f : 1f;
        }

        rb.linearVelocity = new Vector2(pushDir * wallJumpForceX, wallJumpForceY);

        jumpBufferCounter = 0f;
        coyoteCounter = 0f; 

        wallJumpTimer = wallJumpControlTime;
    }

    private bool isWallWallJumpingLockout()
    {
        return wallJumpTimer > 0f;
    }

    private IEnumerator PerformDash()
    {
        canDash = false; 
        isDashing = true;

        rb.gravityScale = 0f;

        Vector2 dashDirection = new Vector2(moveInputX, moveInputY);
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(transform.localScale.x > 0 ? 1f : -1f, 0f);
        }
        else
        {
            dashDirection.Normalize();
        }

        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x * dashEndSpeedMultiplier, rb.linearVelocity.y * dashEndSpeedMultiplier);

        rb.gravityScale = originalGravity;
        isDashing = false;
        
        dashResetTimestamp = Time.time + dashResetDelay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollisions(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollisions(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGroundedNow = false;
        _isTouchingWallNow = false;
    }

    private void EvaluateCollisions(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;

            if (normal.y > 0.7f)
            {
                if (!_isGroundedNow)
                {
                    dashResetTimestamp = Time.time + dashResetDelay;
                }

                _isGroundedNow = true;
            }

            if (Mathf.Abs(normal.x) > 0.7f)
            {
                if ((climbableWallLayer.value & (1 << collision.gameObject.layer)) != 0)
                {
                    _isTouchingWallNow = true;
                }
            }
        }
    }
}
