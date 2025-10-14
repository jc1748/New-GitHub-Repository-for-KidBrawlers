using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 10f;
    public float lifetime = 3f; // seconds before auto-destroy

    private Vector2 direction;

    void Start()
    {
        // Destroy projectile after X seconds
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ignore player
        if (collision.CompareTag("Player")) return;

        //ignore hitting spawner
        if (collision.CompareTag("EnemySpawner")) return;

        Destroy(gameObject);
    }
}