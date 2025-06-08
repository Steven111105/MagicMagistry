using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpray : MonoBehaviour
{
    public float damage = 3f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.TakeDamage(damage, transform);
                enemy.Slowed();
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
