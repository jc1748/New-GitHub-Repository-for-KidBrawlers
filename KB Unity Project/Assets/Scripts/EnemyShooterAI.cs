using UnityEngine;

public class EnemyShooterAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;//who theyre aiming at
    [SerializeField] private Transform firePoint;//where bullets spawn
    [SerializeField] private EnemyProjectile projectilePrefab;//prefab to shoot
    [SerializeField] private Rigidbody2D rb;//rigidbody for movement
    [SerializeField] private EnemyPathfinding pathfindingScript;

    [Header("Ranges")]
    [SerializeField] private float aggroRange = 10f;//how far enemy detects player
    [SerializeField] private float shootRange = 7f;//how far they can shoot
    [SerializeField] private float keepAwayRange = 3f;//if the player gets too close the enemy retreats

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2.5f;

    [Header("Shooting")]
    [SerializeField] private float fireCooldown = 0.8f;// time between shots
    [SerializeField] private LayerMask obstacleMask;//layers that are considered walls
    [SerializeField] private float nextFireTime;//so enemy doesnt shoot every frame

    [Header("Auto-Find Player")]
    [SerializeField] private string playerTag = "Player";
 
    //assigning rigidbodies off start
    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        TryFindPlayer();
    }

    private void TryFindPlayer()
    {
        if(player != null)
        {
            return;
        }

        GameObject p = GameObject.FindGameObjectWithTag(playerTag);
        if (p != null)
        {
            player = p;
        }
    }

    private void SetPathfindingEnabled(bool enabled)
    {
        if (pathfindingScript != null && pathfindingScript.enabled != enabled)
        {
            pathfindingScript.enabled = enabled;
        }
    }

    private void Update()
    {
        TryFindPlayer();

        //if no player reference do nothing
        if (player == null) return;

        //distance from enemy to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //if player is too far away, stop moving and shooting
        if(distanceToPlayer > aggroRange)
        {
            
         
        }

        //direction from enemy to player
        Vector2 directionToPlayer= (player.transform.position - transform.position).normalized;

        //rotate firepoint so bullets face the player, explaination for equation is:
        //the problem I am trying to solve is that the enemy shoots bullets and the bullets need to go towards the player so
        //due to this there needs to be a conversion which will be a direction vector into a rotation angle
        //we get the direction through this: Vector2 directionToPlayer
        //we get the rotation through this: Mathf.Atan2(y,x)
        
        //Mathf.Atan2(directionToPlayer.y, directionToPlayer.x)-this is the conversion for the rotation of the firepoint
        //this function says "if im pointing in this direction, what angle is that?"
        
        //unity uses radians internally for trig math which is the number produced from the previous line
        //Mathf.Rad2Deg- unity rotations use degrees, not radians
        //to convert that into degrees we use:
        //float angle= radians * Mathf.Rad2Deg;
        
        //now we move on to Quaternion.Euler(0,0, angle)
        //in 2D X and Y rotation are not used
        //however Z rotation is used and the one we're concerned with
        
        //firePoint.rotaion= Quaternion.Euler(0,0, angle);
        //this means "rotate this object around the screen so it faces the player"

        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x)* Mathf.Rad2Deg;
        firePoint.rotation= Quaternion.Euler(0,0,angle);

        //check if there is a wall between enemy and player
        bool hasLOS= HasLineOfSight(transform.position, player.transform.position);

        //-------------------------------------MOVEMENT LOGIC-------------------------------------

        if(distanceToPlayer < keepAwayRange)
        {
            //too close so back up
            rb.linearVelocity = -directionToPlayer * moveSpeed;
        }
        else
        {
            //otherwise, stay in place
            
        }

        //-------------------------------------SHOOTING LOGIC-------------------------------------

        if(distanceToPlayer <= shootRange && hasLOS && Time.time >= nextFireTime)
        {
            Shoot(directionToPlayer);
            //set next allowed fire time
            nextFireTime = Time.time + fireCooldown;
        }

       
    }

    //HELPER FUNCTIONS

    //returns true if no obstacle is between "from" and "to"
    private bool HasLineOfSight(Vector2 from, Vector2 to)//if i draw a straight line from the enemy do the player, does it hit a wall?
    {
        //direction and distance for raycast
        //to-from creates a vector that points from the enemy toward the player
        Vector2 direction = (to - from).normalized;//vector2 from- this is where the ray starts(enemy position); vector 2 to- what we're checking visibility to (player position)
        float distance = Vector2.Distance(from, to);

        //shoot an invisible laser from enemy toward player, stop at player distance, and tell me if it hits a wall
        //raycast to see if a wall blocks vision
        RaycastHit2D hit = Physics2D.Raycast(from, direction, distance, obstacleMask);
        return hit.collider == null;//if nothing was hit, there is a clean LOS
    }

    private void Shoot(Vector2 direction)
    {
        //Spawn a bullet at the fire point
        EnemyProjectile projectile= Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        //fire it toward player
        projectile.Fire(direction);
    }

    private void OnDrawGizmosSelected()
    {
        // Aggro range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        // Shoot range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);

        // Keep-away range
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, keepAwayRange);

        // Draw a LOS line if we have a player
        if (player != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }

}
