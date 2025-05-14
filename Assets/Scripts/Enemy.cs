using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    SpriteRenderer sr;
    public Rigidbody2D rb;
    [SerializeField] protected float speed;
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected bool isTakingKnockback;
    [Header("Resistance is a float between 0 and 1 (Percentage)")]
    [Tooltip("0.3 is 30% kb resistance")]
    [SerializeField] protected float knockbackResistance = 0.5f; 

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public abstract void Move();
    
    public void TakeDamage(float damage, Transform source = null, float knockbackForce = 0)
    {
        if(source != null)
        {
            Debug.Log("Knockback from " + source.name);
            Vector3 direction = (transform.position-source.position).normalized;
            // Add knockback effect here if needed
            StopCoroutine(nameof(ApplyKnockback));
            StartCoroutine(ApplyKnockback(direction, knockbackForce, knockbackResistance, 0.2f));
        }
        // Handle enemy taking damage here
        Debug.Log("Enemy took " + damage + " damage!");
        health -= damage;
        StopCoroutine(nameof(DamageFlash));
        StartCoroutine(DamageFlash());
    }
    IEnumerator ApplyKnockback(Vector3 direction, float knockbackForce, float knockbackResistance, float duration)
    {
        isTakingKnockback = true; // Disable movement during knockback
        float timer = 0;
        duration = duration * (1-knockbackResistance);
        while (timer < duration){
            rb.MovePosition(rb.position + (Vector2)(direction * knockbackForce * Time.fixedDeltaTime));
            timer += Time.deltaTime ;
            yield return null;
        }
        Debug.Log("Knockback ended");
        isTakingKnockback = false; // Re-enable movement after knockback
    }
       

    IEnumerator DamageFlash(){
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
