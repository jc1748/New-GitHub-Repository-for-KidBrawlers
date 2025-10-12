using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    void Start()
    {
        // Get Rigidbody2D and Animator components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get player input using arrow keys
        moveInput.x = Input.GetAxisRaw("Horizontal"); // Left/Right arrows
        moveInput.y = Input.GetAxisRaw("Vertical");   // Up/Down arrows

        // Normalize so diagonal movement isn't faster
        moveInput = moveInput.normalized;

        // Update animator parameters if you have running animations
        if (animator != null)
        {
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
            animator.SetFloat("Speed", moveInput.sqrMagnitude);
            animator.SetTrigger("Rolling");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Rolling");
        }
    }

    void FixedUpdate()
    {
        // Apply movement using Rigidbody2D
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
