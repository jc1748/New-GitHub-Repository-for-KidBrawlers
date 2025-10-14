using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //health set up
    public float maxHealth = 5f;
    private float currentHealth;

    //health bar
    public Transform healthBar;
    private Vector3 originalScale;

    //invincibility frames
    public float invincibilityDuration = 1.0f;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            originalScale = healthBar.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if(invincibilityTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            Debug.Log("Player is invincible — no damage taken");
            return;
        }

        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage! Current health: {currentHealth}");

        // Update the health bar
        if (healthBar != null)
        {
            float healthPercent = Mathf.Clamp01((float)currentHealth / maxHealth);
            healthBar.localScale = new Vector3(originalScale.x * healthPercent, originalScale.y, originalScale.z);
        }

        // Trigger i-frames
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;

        // Handle death
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }


}
