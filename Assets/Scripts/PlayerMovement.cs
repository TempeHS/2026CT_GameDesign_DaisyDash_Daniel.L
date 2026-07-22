using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private PlayerControls controls;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    [SerializeField] private float maxSpeed = 9f;
    [SerializeField] private float acceleration = 90f;
    [SerializeField] private float deceleration = 60f;
    private Vector2 moveInput;

    [Header("Jump Mechanics")]
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float gravityScale = 4f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private bool isJumpHeld;

    [Header("Dash Mechanics")]
    [SerializeField] private float dashSpeed = 24f;
    [SerializeField] private float dashTime = 0.15f;
    [SerializeField] private float dashCooldown = 0.2f;
    private bool canDash = true;
    private bool isDashing;

    private void Awake()
    {   
        controls = new PlayerControls();

        controls.Player.Jump.started += ctx => OnJumpStarted();
        controls.Player.Jump.canceled += ctx => OnJumpCanceled();

        controls.Player.Dash.started += ctx => OnDashStarted();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        rb.gravityScale = gravityScale;
    }

    void Update()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            canDash = true; 
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteCounter > 0f)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        Run();
        ModifyPhysics();
    }
    private void OnJumpStarted()
    {
        jumpBufferCounter = jumpBufferTime;
        isJumpHeld = true;
    }

    private void OnJumpCanceled()
    {
        isJumpHeld = false;
    }

    private void OnDashStarted()
    {
        if (canDash && !isDashing)
        {
            StartCoroutine(PerformDash());
        }
    }

    private void Run()
    {
        float targetSpeed = moveInput.x * maxSpeed;
        float speedDif = targetSpeed - rb.linearVelocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = speedDif * accelRate;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private void ModifyPhysics()
    {

        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallMultiplier;
        }
        else if (rb.linearVelocity.y > 0 && !isJumpHeld)
        {
            rb.gravityScale = gravityScale * lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    private bool isGrounded;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
      {
          isGrounded = true;
      }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
     if (collision.gameObject.CompareTag("Ground"))
      {
         isGrounded = false;
      }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpBufferCounter = 0f;
        coyoteCounter = 0f;
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        isDashing = true;
        
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        Vector2 dashDir = moveInput.normalized;
        if (dashDir == Vector2.zero) 
        {
            dashDir = new Vector2(transform.localScale.x > 0 ? 1 : -1, 0);
        }

        rb.linearVelocity = dashDir * dashSpeed;
        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();
}