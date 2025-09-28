using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public int maxHealth = 5; // hits until destroyed
    private int currentHealth;

    public Transform healthBar; // assign the HealthBar child here

    private Vector3 originalHealthScale;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            originalHealthScale = healthBar.localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only count objects with Projectile script
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
        {
            currentHealth--;
            Debug.Log("Box hit! Remaining health: " + currentHealth);

            Destroy(other.gameObject); // destroy the projectile

            // Update health bar
            if (healthBar != null)
            {
                float healthPercent = (float)currentHealth / maxHealth;
                healthBar.localScale = new Vector3(originalHealthScale.x * healthPercent,
                                                   originalHealthScale.y,
                                                   originalHealthScale.z);
            }

            // Destroy the box if health is zero
            if (currentHealth <= 0)
            {
                Debug.Log("Box destroyed!");
                Destroy(gameObject);
            }
        }
    }
}