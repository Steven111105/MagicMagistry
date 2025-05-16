using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth = 10f;
    public float health;
    public int level;
    public Slider expBar;
    public float nextLevel = 10f;
    public float exp;
    public float speed = 5f;
    public float attackDamage = 1f;
    public float reflectDamage = 1f;
    public float attackSpeed = 1f;
    bool allowRegen = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // Set initial health
        exp = 0; // Set initial experience
        healthBar.maxValue = maxHealth; // Set max value for health bar
        healthBar.value = health; // Set initial value for health bar
        expBar.maxValue = nextLevel; // Set max value for experience bar
        expBar.value = exp; // Set initial value for experience bar
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage){
        allowRegen = false;
        health -= damage;
        healthBar.value = health / maxHealth;
        if(health <= 0){
            Debug.Log("Player is dead");
        }
        Invoke(nameof(AllowRegen), 3f);
    }

    void AllowRegen(){
        allowRegen = true;
        StartCoroutine(RegenHP());
    }

    IEnumerator RegenHP(){
        while(allowRegen){
            health += 0.2f;
            healthBar.value = health / maxHealth;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
