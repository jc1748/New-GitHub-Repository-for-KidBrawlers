using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public int maxHits = 5;
    private int currentHits = 0;

    [Header("Health Bar")]
    public Transform healthBar;   // assign a child object (sprite) in Inspector
    private Vector3 originalScale;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (healthBar != null)
        {
            originalScale = healthBar.localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject); // remove projectile
            TakeDamage(1);

        }
    }

    public void TakeDamage(int damage)
    {
        currentHits += damage;
        Debug.Log("Enemy took damage! Current hits: " + currentHits);

        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        if (healthBar != null)
        {
            float healthPercent = Mathf.Clamp01(1f - ((float)currentHits / maxHits));
            healthBar.localScale = new Vector3(originalScale.x * healthPercent, originalScale.y, originalScale.z);
        }

        if (currentHits >= maxHits)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Enemy defeated!");
        if (CompareTag("Boss"))
        {
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.WinGame();
            }
        }
        Destroy(gameObject);
    }

}