using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    
    public Transform healthBar;
    private Vector3 originalScale;

    public float iFrameDuration = 1f;
    private bool isInvincible = false;
    private float iFrameTimer;

    private Animator animator;
    private void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

        if (healthBar != null)
        {
           originalScale = healthBar.localScale;
        }
            
    }

    private void Update()
    {
        if (isInvincible)
        {
            iFrameTimer -= Time.deltaTime;
            if (iFrameTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (animator != null)
        {
            animator.SetTrigger("Hurt"); // <-- Play the hurt animation
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        if (healthBar != null)
        {
            float healthPercent = Mathf.Clamp01((float)currentHealth / maxHealth);
            healthBar.localScale = new Vector3(originalScale.x * healthPercent, originalScale.y, originalScale.z);
        }

        // Start invincibility frames
        isInvincible = true;
        iFrameTimer = iFrameDuration;
    }

    private void Die()
    {
        Debug.Log("Player died!");
        FindFirstObjectByType<GameManager>().EndGame();
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }
}
