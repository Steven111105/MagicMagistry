using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int enemiesKilled; // Static variable to keep track of enemies killed
    public Slider healthBar;
    public float maxHealth = 10f;
    public float health;
    public int level;
    // public Slider expBar;
    // public float nextLevel = 10f;
    // public float exp;
    public float speed = 5f;
    public float attackDamage = 1f;
    public float reflectDamage = 1f;
    public float attackSpeed = 1f;
    bool allowRegen = false;
    PlayerAnim playerAnim;
    void OnEnable()
    {
        playerAnim = transform.GetChild(0).GetComponent<PlayerAnim>();
        enemiesKilled = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // Set initial health
        healthBar.value = health; // Set initial value for health bar
        // exp = 0; // Set initial experience
        // expBar.maxValue = nextLevel; // Set max value for experience bar
        // expBar.value = exp; // Set initial value for experience bar
    }

    public void TakeDamage(float damage){
        allowRegen = false;
        health -= damage;
        healthBar.value = health / maxHealth;
        if (health <= 0)
        {
            Debug.Log("Player is dead");
            playerAnim.Die();
            GetComponent<PlayerMovement>().canMove = false;
        }
        Invoke(nameof(AllowRegen), 3f);
    }

    void AllowRegen(){
        allowRegen = true;
        StopCoroutine(RegenHP());
        StartCoroutine(RegenHP());
    }

    IEnumerator RegenHP(){
        while(allowRegen){
            health += 0.1f;
            if(health >= maxHealth)
            {
                health = maxHealth; // Cap health at maxHealth
                healthBar.value = 1f; // Set health bar to full 
                yield break; // Stop regenerating if health is full
            }
            healthBar.value = health / maxHealth;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
