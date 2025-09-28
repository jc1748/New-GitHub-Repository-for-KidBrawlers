using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public int maxHealth = 5; // how many hits until destroyed
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            currentHealth--;
            Debug.Log("Box hit! Remaining health: " + currentHealth);

            Destroy(other.gameObject); // remove projectile

            if (currentHealth <= 0)
            {
                Debug.Log("Box destroyed!");
                Destroy(gameObject); // destroy this box
            }
        }
    }
}