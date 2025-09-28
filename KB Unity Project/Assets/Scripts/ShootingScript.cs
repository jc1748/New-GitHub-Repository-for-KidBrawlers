using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint; // empty GameObject where projectiles spawn
    public float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click to shoot
        {
            Shoot();
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
    }
}
