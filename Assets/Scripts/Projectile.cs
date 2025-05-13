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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Shoot(Vector2.right, speed, isReflectable, damage);
    }

    public void Shoot(Vector2 direction, float speed, bool isReflectable,float damage)
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90f);
        rb.velocity = direction.normalized * speed;
        this.isReflectable = isReflectable;
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bullet collided with " + collision.gameObject.name);
        if(!isEnemyProjectile){
            collision.gameObject.GetComponent<Enemy>()?.TakeDamage(damage);
            Destroy(gameObject);
        }else{
            //if not dashing, dont destroy the bullet/take damage
            if(!collision.gameObject.GetComponent<PlayerDash>().isDashing){
                collision.gameObject.GetComponent<PlayerStats>()?.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
    public void Reflect(float angle){
        if(isReflectable && isEnemyProjectile){
            //scuffed maths to reflect the buller according to the slash angle
            transform.localEulerAngles = new Vector3(0, 0, angle+90f);  
            angle = angle * Mathf.Deg2Rad;
            Vector2 direction = -new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            rb.velocity =  direction * speed;
            isEnemyProjectile = false;
        }
    }
}
