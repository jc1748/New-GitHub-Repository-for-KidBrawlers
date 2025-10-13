using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint; // empty GameObject where projectiles spawn
    public float projectileSpeed = 10f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // <-- Get Animator from Player
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click to shoot
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play the animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

    }

    void Shoot()
    {
        // Convert mouse position to world position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Calculate direction from player to mouse
        Vector2 shootDir = (mousePos - firePoint.position).normalized;

        // Spawn projectile
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Apply movement
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDir * projectileSpeed;
        }

        //Prevent bullet from colliding with player
        Collider2D playerCol = GetComponent<Collider2D>();
        Collider2D projCol = proj.GetComponent<Collider2D>();
        if (playerCol != null && projCol != null) 
        {
            Physics2D.IgnoreCollision(projCol, playerCol);
        }
        
    }
}
