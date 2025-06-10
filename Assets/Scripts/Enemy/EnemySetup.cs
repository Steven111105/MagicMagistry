using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySetup : MonoBehaviour
{
    public EnemySpawner spawner;
    int waveNumber;
    public Sprite sprite;
    public void SetupEnemy(int enemyType)
    {
        waveNumber = spawner.waveNumber;
        switch (enemyType)
        {
            case 0:
                sprite = spawner.chaseEnemySprite;
                float health = 5 + Mathf.CeilToInt(waveNumber * 1.3f);
                float speed = 3;
                float damage = 1 + (waveNumber / 5);
                SetupChaseEnemy(spawner.player, health, speed, damage);
                break;
            case 1:
                sprite = spawner.shootingEnemySprite;
                health = 3 + Mathf.Round(waveNumber * 1.2f);
                speed = 2;
                damage = 3 + (waveNumber / 5);
                float shootingRange = 12f;
                float shootingCooldown = 1f;
                float bulletSpeed = 6f;
                SetupShootingEnemy(spawner.player, health, speed, damage, shootingRange, shootingCooldown, bulletSpeed);
                break;
            case 2:
                sprite = spawner.slimeEnemySprite;
                health = 10 + waveNumber;
                speed = 3f;
                damage = 5 + (waveNumber / 5);
                float moveDuration = 0.5f;
                float moveCooldown = 0.5f;
                SetupSlimeEnemy(spawner.player, health, speed, damage, moveDuration, moveCooldown, 2);
                break;
            default:
                Debug.LogError("Invalid enemy type");
                break;
        }
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void SetupChaseEnemy(Transform player, float health, float speed, float damage)
    {
        gameObject.AddComponent<FollowingEnemy>();
        Enemy enemy = gameObject.GetComponent<FollowingEnemy>();
        // enemy.sr.color = Color.blue;
        enemy.player = player;
        enemy.health = health;
        enemy.speed = speed;
        enemy.damage = damage;
        enemy.damageTextPrefab = spawner.damageTextPrefab;
        enemy.currentSpeed = speed;
    }

    public void SetupShootingEnemy(Transform player, float health, float speed, float damage,
                                float shootingRange, float shootingCooldown, float bulletSpeed)
    {
        gameObject.AddComponent<ShootingEnemy>();
        ShootingEnemy enemy = gameObject.GetComponent<ShootingEnemy>();
        // enemy.sr.color = Color.green;
        enemy.player = player;
        enemy.health = health;
        enemy.speed = speed;
        enemy.damage = damage;
        enemy.shootingRange = shootingRange;
        enemy.shootingCooldown = shootingCooldown;
        enemy.bulletSpeed = bulletSpeed;
        enemy.bulletPrefab = spawner.bulletPrefab;
        enemy.damageTextPrefab = spawner.damageTextPrefab;
        enemy.currentSpeed = speed;
    }

    public void SetupSlimeEnemy(Transform player, float health, float speed, float damage,
                                float moveDuration, float moveCooldown, float size)
    {
        if (gameObject.GetComponent<SlimeEnemy>() != null)
        {
            Destroy(gameObject.GetComponent<SlimeEnemy>());
        }

        SlimeEnemy enemy = gameObject.AddComponent<SlimeEnemy>(); ;
        // enemy.sr.color = Color.yellow;
        enemy.maxHealth = health;
        enemy.player = player;
        enemy.health = health;
        enemy.speed = speed;
        enemy.damage = damage;
        enemy.moveDuration = moveDuration;
        enemy.moveCooldown = moveCooldown;
        enemy.size = size;
        enemy.transform.localScale = new Vector3(size, size, 1); // Scale based on size
        float canvasSize = 0.02f / size;
        transform.localScale = new Vector3(size, size, 1); // Scale the enemy object
        enemy.damageTextPrefab = spawner.damageTextPrefab;
        enemy.currentSpeed = speed;
    }
}
