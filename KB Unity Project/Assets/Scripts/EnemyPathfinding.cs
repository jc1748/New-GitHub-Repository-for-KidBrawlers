using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Transform player;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this makes it not collide with spawner
        if (collision.gameObject.CompareTag("EnemySpawner")) return;

        //this makes it not collide with houses
        if (collision.gameObject.CompareTag("Houses")) return;

        //this makes it not collide with enemies
        if (collision.gameObject.CompareTag("Enemy")) return;

        //objects
        if (collision.gameObject.CompareTag("Objects")) return;

    }


    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            //flip enemy based on player position
            if (direction.x !=0)
            {
                //if player is to the right face right, vice versa
                transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
            }

            // Update animator values
            if (animator != null)
            {
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
                animator.SetFloat("Speed", direction.sqrMagnitude);
            }
        }
    }


}
