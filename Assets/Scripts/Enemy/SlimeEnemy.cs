using System.Collections;
using UnityEngine;

public class SlimeEnemy : Enemy
{
    public float maxHealth;
    public float moveDuration = 0.5f;
    public float moveCooldown = 0.5f;
    [SerializeField] private bool canMove = true;
    public float size;
    bool canAttack = true;
    [SerializeField] float attackCooldown = 1f;
    // Start is called before the first frame update
    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(MoveCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.enabled == false)
        {
            Destroy(gameObject);
            return; // Exit if the sprite renderer is disabled
        }
        if (!isTakingKnockback)
        {
            if (canAttack)
            {
                if (canMove)
                {
                    Move();
                }
                else
                {
                    rb.velocity = Vector2.zero; // Stop movement if not allowed
                }
            }
            else
            {
                rb.velocity = Vector2.zero; // Stop movement if not allowed
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
        if (canMove)
        {
            if (player != null)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * currentSpeed;
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop movement if not allowed
            return; // Exit the method to prevent further movement
        }
    }

    IEnumerator MoveCooldown()
    {
        while(true)
        {
            // Debug.Log("SlimeEnemy is moving");
            canMove = true; // Allow movement
            yield return new WaitForSeconds(moveDuration);
            // Debug.Log("SlimeEnemy is not moving");
            canMove = false; // Allow movement
            yield return new WaitForSeconds(moveCooldown);
        }
    }

    public override void Die()
    {
        if (size != 0.5f)
        {
            Invoke(nameof(Split), 0.3f);
        }
        else
        {
            Destroy(gameObject);
            PlayerStats.enemiesKilled++;
            Debug.Log("SlimeEnemy died");
        }
    }

    public override void ShowDamageText(float damage)
    {
        base.ShowDamageText(damage);
        transform.GetChild(0).localScale = new Vector2(0.02f / size, 0.02f / size);
    }

    void Split()
    {
        foreach (Transform child in transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
        GameObject slime1 = Instantiate(gameObject, transform.position + transform.right * 0.1f, Quaternion.identity);
        GameObject slime2 = Instantiate(gameObject, transform.position + transform.right * 0.1f, Quaternion.identity);
        spawner.enemies.Add(slime1);
        spawner.enemies.Add(slime2);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        sr.enabled = false;
        if (size == 1)
        {
            slime1.GetComponent<EnemySetup>().SetupSlimeEnemy(player, Mathf.CeilToInt(maxHealth / 2), speed, damage, moveDuration, moveCooldown, 0.5f);
            slime2.GetComponent<EnemySetup>().SetupSlimeEnemy(player, Mathf.CeilToInt(maxHealth / 2), speed, damage, moveDuration, moveCooldown, 0.5f);
        }
        else
        {
            slime1.GetComponent<EnemySetup>().SetupSlimeEnemy(player, Mathf.CeilToInt(maxHealth / 2), speed, damage, moveDuration, moveCooldown, size - 1);
            slime2.GetComponent<EnemySetup>().SetupSlimeEnemy(player, Mathf.CeilToInt(maxHealth / 2), speed, damage, moveDuration, moveCooldown, size - 1);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Enemy collided with player");
            collision.gameObject.GetComponent<PlayerStats>()?.TakeDamage(damage);
            canAttack = false;
            rb.velocity = Vector2.zero; // Stop the enemy's movement
            Invoke(nameof(AllowAttack), attackCooldown);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero; // Stop the enemy's movement
        if (collision.gameObject.CompareTag("Player") && canAttack)
        {
            collision.gameObject.GetComponent<PlayerStats>()?.TakeDamage(damage);
            canAttack = false;
            Invoke(nameof(AllowAttack), attackCooldown);
        }
    }

    void AllowAttack()
    {
        canAttack = true;
    }
}
