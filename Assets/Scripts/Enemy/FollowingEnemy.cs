using UnityEngine;

public class FollowingEnemy : Enemy
{
    bool canAttack = true;
    [SerializeField] float attackCooldown = 1f;

    // Update is called once per frame

    void Update()
    {
        if (!isTakingKnockback)
        {
            if (canAttack)
            {
                Move();
            }
        }
        if (!flashing)
        {
            // Debug.Log("Enemy is not flashing, change to white");
            sr.color = new Color(255, 255, 255); // Reset color to white
        }
        
    }
    public override void Move()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * currentSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Enemy collided with player");
            collision.gameObject.GetComponent<PlayerStats>()?.TakeDamage(damage);
            canAttack = false;
            rb.velocity = Vector2.zero; // Stop the enemy's movement
            Invoke(nameof(AllowAttack), attackCooldown);
        }
    }

    void AllowAttack()
    {
        canAttack = true;
    }
}
