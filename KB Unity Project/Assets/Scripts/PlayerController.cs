using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Normal movement speed

    public float rollDuration = 0.5f; // How long the roll lasts (i-frames)
    public float rollCooldown = 1f;   // Time between rolls
    public float rollForce = 4f; //How strong the dash is

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    // Roll state
    private bool isRolling = false;
    private float rollTimer = 0f;
    private float cooldownTimer = 0f;

    // Invincibility
    private bool isInvincible = false;

    private Collider2D playerCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Get movement input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        //incase of any diagonal movement
        moveInput = moveInput.normalized;

        // Update animator for movement
        if (animator != null)
        {
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
            animator.SetFloat("Speed", moveInput.sqrMagnitude);
        }

        // Handle roll input (only if not currently rolling and cooldown is over)
        if (Input.GetKeyDown(KeyCode.Space) && !isRolling && cooldownTimer <= 0f)
        {
            StartRoll();
        }

        // Update timers
        if (isRolling)
        {
            //subtracts the time that passed this frame from the remaining roll time
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0f)
            {
                EndRoll();
            }
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // Only allow normal movement if not rolling
        if (!isRolling)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void StartRoll()
    {   //if player doesn't have a direction when moving, sets the default to up
        if(moveInput == Vector2.zero)
        {
            moveInput = Vector2.up;
        }
        isRolling = true;
        isInvincible = true;
        rollTimer = rollDuration;
        cooldownTimer = rollCooldown;

        animator.SetTrigger("Rolling");

        // Force for roll
        //ForceMode2D.Impulse adds the instant one time burst of energy--makes character immediately launch in direction
        rb.AddForce(moveInput * rollForce, ForceMode2D.Impulse);
    }

    private void EndRoll()
    {
        isRolling = false;
        isInvincible = false;
        //stop velocity to end roll abruptly
        rb.linearVelocity= Vector2.zero;
    }

    // Example method to check if player can take damage
    public bool CanTakeDamage()
    {
        return !isInvincible;
    }
}
