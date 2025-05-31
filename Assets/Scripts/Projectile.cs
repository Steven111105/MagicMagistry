using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    public float speed;
    public bool isReflectable;
    public float damage;
    public bool isEnemyProjectile;
    float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Shoot(Vector2 direction, float speed, bool isReflectable, float damage, bool isEnemyProjectile = true)
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
        rb.velocity = direction.normalized * speed;
        this.speed = speed;
        this.isReflectable = isReflectable;
        this.damage = damage;
        this.isEnemyProjectile = isEnemyProjectile;
        SetTagAndLayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 20f){
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
        SetTagAndLayer();
    }

    void SetTagAndLayer(){
        if(isEnemyProjectile){
            gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
            gameObject.tag = "EnemyProjectile";
            sr.color = Color.red;

        }else{
            gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
            gameObject.tag = "PlayerProjectile";
            sr.color = Color.yellow;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))){
            //if its not colliding with player or enemy, ignore
            return;
        }
        // Debug.Log("bullet collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Wall"))
        {
            //if its colliding with wall, destroy the bullet
            Destroy(gameObject);
            return;
        }

        if (!isEnemyProjectile)
        {
            //Not enemy projectile, so its from player
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>()?.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            //if not dashing, dont destroy the bullet/take damage
            collision.gameObject.GetComponent<PlayerStats>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if(!(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))){
            //if its not colliding with player or enemy, ignore
            return;
        }
        Debug.Log("bullet collided with " + collision.gameObject.name);
        if(!isEnemyProjectile){
            //Not enemy projectile, so its from player
            if(collision.gameObject.CompareTag("Enemy")){
                collision.gameObject.GetComponent<Enemy>()?.TakeDamage(damage);
                Destroy(gameObject);
            }
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
            isEnemyProjectile = false;
            damage = 1f;
        }
    }
}
