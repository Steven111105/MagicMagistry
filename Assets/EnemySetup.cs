using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    public EnemySpawner spawner;
    int waveNumber;
    public void SetupEnemy(int enemyType)
    {
        waveNumber = spawner.waveNumber;
        switch (enemyType)
        {
            case 0:
                int health = 10 + (waveNumber * 2);
                int speed = 3;
                int damage = 1 + (waveNumber / 2);
                SetupChaseEnemy(spawner.player, health, speed, damage);
                break;
            case 1:
                health = 5 + (waveNumber * 2);
                speed = 2;
                damage = 5 + (waveNumber / 2);
                float shootingRange = 5f + (waveNumber * 0.5f);
                float shootingCooldown = 1f;
                float bulletSpeed = 6f;
                SetupShootingEnemy(spawner.player, health, speed, damage, shootingRange, shootingCooldown, bulletSpeed);
                break;
            default:
                Debug.LogError("Invalid enemy type");
                break;
        }
    }
    public void SetupChaseEnemy(Transform player, float health, float speed, float damage)
    {
        gameObject.AddComponent<FollowingEnemy>();
        Enemy enemy = gameObject.GetComponent<FollowingEnemy>();
        enemy.Setup();
        enemy.sr.color = Color.blue;
        enemy.player = player;
        enemy.health = health;
        enemy.speed = speed;
        enemy.damage = damage;
    }

    public void SetupShootingEnemy(Transform player, float health, float speed, float damage, 
                                float shootingRange, float shootingCooldown, float bulletSpeed){
        gameObject.AddComponent<ShootingEnemy>();
        ShootingEnemy enemy = gameObject.GetComponent<ShootingEnemy>();
        enemy.Setup();
        enemy.sr.color = Color.green;
        enemy.player = player;
        enemy.health = health;
        enemy.speed = speed;
        enemy.damage = damage;
        enemy.shootingRange = shootingRange;
        enemy.shootingCooldown = shootingCooldown;
        enemy.bulletSpeed = bulletSpeed;
        enemy.bulletPrefab = spawner.bulletPrefab;
    }
}
