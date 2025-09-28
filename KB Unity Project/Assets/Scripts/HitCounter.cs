using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public int maxHits = 5;
    private int currentHits = 0;

    [Header("Health Bar")]
    public Transform healthBar;   // assign a child object (sprite) in Inspector
    private Vector3 originalScale;

    private void Start()
    {
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
            currentHits++;

            Debug.Log("Box hit! Current hits: " + currentHits);

            // Update health bar
            if (healthBar != null)
            {
                float healthPercent = Mathf.Clamp01(1f - ((float)currentHits / maxHits));
                healthBar.localScale = new Vector3(originalScale.x * healthPercent,
                                                   originalScale.y,
                                                   originalScale.z);
            }

            if (currentHits >= maxHits)
            {
                Destroy(gameObject); // destroy box
            }
        }
    }
}