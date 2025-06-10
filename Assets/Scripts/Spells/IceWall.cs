using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.3742138f, 1f, 0.949218f, 1f);
        StartCoroutine(RemoveWall());
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.Freeze();
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.Freeze();
            }
        }
    }
    
    IEnumerator RemoveWall()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
