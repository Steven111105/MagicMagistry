using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100f; // Set initial health
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            Debug.Log("Player is dead");
            // Add death logic here
        }
    }
}
