using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //health set up
    public int maxHealth = 8;
    private int currentHealth;
    public Slider slider;

    //invincibility frames
    public float invincibilityDuration = 1.0f;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    private Animator animator;
    private void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

        if (slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = currentHealth;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
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

        else
        {
            currentHealth -= damage;
            slider.value = currentHealth;
        }

        if (animator != null)
        {
            animator.SetTrigger("Hurt"); // <-- Play the hurt animation
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        // Trigger i-frames
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }
    private void Die()
    {
        Debug.Log("Player died!");
        FindFirstObjectByType<GameManager>().EndGame();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }


}