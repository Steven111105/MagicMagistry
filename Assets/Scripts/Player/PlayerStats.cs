using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth = 10f;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // Set initial health
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage){
        health -= damage;
        healthBar.value = health / maxHealth;
        if(health <= 0){
            Debug.Log("Player is dead");
            // Add death logic here
        }
    }
}
