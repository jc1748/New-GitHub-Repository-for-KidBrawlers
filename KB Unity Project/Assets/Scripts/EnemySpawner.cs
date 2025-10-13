using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Prototype2;
    public Transform spawnPoint;
    public int maxEnemies = 3;
    public float spawnDelay = 0.5f;

    private int enemiesSpawned = 0;
    private bool playerInside = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        //check if player entered
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            TrySpawnEnemy();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInside= false;
            }
        }

        void TrySpawnEnemy()
        {
            if (enemiesSpawned < maxEnemies)
            {
                Instantiate(Prototype2, spawnPoint.position, Quaternion.identity);
                enemiesSpawned++;

                //delay before spawning again
                if(playerInside && enemiesSpawned < maxEnemies)
                {
                    Invoke(nameof(TrySpawnEnemy), spawnDelay);
                }

            }
        }

     
    }
}
