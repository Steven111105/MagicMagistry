using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10f;
    public float damage = 1f;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(float damage)
    {
        // Handle enemy taking damage here
        Debug.Log("Enemy took " + damage + " damage!");
        health -= damage;
        StopAllCoroutines();
        StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash(){
        Debug.Log("Damage Flash Coroutine");
        float duration = 0.3f;
        float t = 0;
        while(t < duration){
            sr.color = new Color(255,0,0);
            t += Time.deltaTime;
            yield return null;
        }
        t = 0;
        while(t < duration){
            sr.color = new Color(255,255,255);
            t += Time.deltaTime;
            yield return null;
        }
        if(health < 0){
            Die();
        }
    }

    void Die()
    {
        // Handle enemy death here
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
