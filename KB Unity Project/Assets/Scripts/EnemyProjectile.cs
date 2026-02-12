using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //lets bullet travel in directions
    public void Fire(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Prevent bullet from dying on enemy itself
        if (other.CompareTag("Enemy"))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            //update damage for player
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            //when it collides destroy game object
            Destroy(gameObject);
            return;

        }

        //destroy on walls
        if(other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Destroy(gameObject);
        }

    }
   
}
