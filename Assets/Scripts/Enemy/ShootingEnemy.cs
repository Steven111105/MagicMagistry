using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField] float shootingRange = 5f;
    [SerializeField] float shootingCooldown = 1f;
    float shootProgress;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletDamage = 5f;
    [SerializeField] GameObject bulletPrefab;
    bool isWaiting = false;
    // Update is called once per frame
    void Update(){
        //if distance not in range, move towards player
        if (Vector3.Distance(transform.position, player.position) > shootingRange)
        {
            if (isWaiting)
            {
                shootProgress = 0;
            }
            isWaiting = false;
            Move();
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop the enemy's movement
            if (isWaiting)
            {
                shootProgress += Time.deltaTime;
                if (shootProgress >= shootingCooldown)
                {
                    Shoot();
                    shootProgress = 0;
                }
            }
            isWaiting = true;
        }
    }

    public override void Move(){
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().Shoot((player.position - transform.position).normalized, bulletSpeed, true, bulletDamage);
    }
        
}
