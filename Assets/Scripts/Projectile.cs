using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public bool isReflectable;
    public float damage;
    public bool isEnemyProjectile;
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 direction, float speed, bool isReflectable, float damage, bool isEnemyProjectile = true)
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
        rb.velocity = direction.normalized * speed;
        this.speed = speed;
        this.isReflectable = isReflectable;
        this.damage = damage;
        this.isEnemyProjectile = isEnemyProjectile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))){
            //if its not colliding with player or enemy, ignore
            return;
        }
        Debug.Log("bullet collided with " + collision.gameObject.name);
        if(!isEnemyProjectile){
            //Not enemy projectile, so its from player
            collision.gameObject.GetComponent<Enemy>()?.TakeDamage(damage);
            Destroy(gameObject);
        }else{
            //if not dashing, dont destroy the bullet/take damage
            if(collision.gameObject.GetComponent<PlayerDash>()?.isDashing == false){
                collision.gameObject.GetComponent<PlayerStats>()?.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
    public void Reflect(float angle){
        if (isReflectable && isEnemyProjectile)
        {
            //scuffed maths to reflect the buller according to the slash angle
            transform.localEulerAngles = new Vector3(0, 0, angle + 90f);
            angle *= Mathf.Deg2Rad;
            Vector2 direction = -new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            rb.velocity = direction.normalized * speed;
            this.isEnemyProjectile = false;
        }
    }
}
