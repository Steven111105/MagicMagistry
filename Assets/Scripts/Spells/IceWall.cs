using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
     void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.Slowed();
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.Slowed();
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
