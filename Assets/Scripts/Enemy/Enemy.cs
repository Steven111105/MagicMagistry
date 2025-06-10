using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public GameObject damageTextPrefab;
    [SerializeField] public float speed;
    [SerializeField] public float currentSpeed;
    [SerializeField] public float health;
    [SerializeField] public float damage;
    [SerializeField] public bool isTakingKnockback;
    [Header("Resistance is a float between 0 and 1 (Percentage)")]
    [Tooltip("0.3 is 30% kb resistance")]
    [SerializeField] public float knockbackResistance = 0.5f; 
    [SerializeField] protected bool flashing = false;
    public EnemySpawner spawner;
    public virtual void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public abstract void Move();

    public void TakeDamage(float damage, Transform source = null, float knockbackForce = 0)
    {
        if(damage <= 0)
        {
            return;
        }
        if (source != null)
        {
            // Debug.Log("Knockback from " + source.name);
            Vector3 direction = (transform.position - source.position).normalized;
            // Add knockback effect here if needed
            StopCoroutine(nameof(ApplyKnockback));
            StartCoroutine(ApplyKnockback(direction, knockbackForce, knockbackResistance, 0.2f));
        }
        // Handle enemy taking damage here
        // Debug.Log("Enemy took " + damage + " damage!");
        health -= damage;
        ShowDamageText(damage);
        StopCoroutine(nameof(DamageFlash));
        sr.color = new Color(255, 255, 255); // Reset color to white
        StartCoroutine(DamageFlash());
    }

    public virtual void ShowDamageText(float damage)
    {
        GameObject damageText = Instantiate(damageTextPrefab,Vector2.zero, Quaternion.identity, transform.GetChild(0));
        damageText.GetComponent<TMP_Text>().text = damage.ToString();
        damageText.transform.localPosition = new Vector3(0, 0.5f, 0);
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
        // Debug.Log("Knockback ended");
        isTakingKnockback = false; // Re-enable movement after knockback
    }

    IEnumerator DamageFlash(){
        flashing = true;
        float duration = 0.1f;
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
        flashing = false;
        if(health <= 0){
            Die();
        }
    }

    public void Slowed()
    {
        currentSpeed = speed * 0.25f; // Reduce speed by 50%
        StopCoroutine(nameof(ResetSpeed));
        StartCoroutine(nameof(ResetSpeed));
    }

    public void Freeze()
    {
        currentSpeed = 0; // Stop movement
        StopCoroutine(nameof(ResetFreeze));
        StartCoroutine(nameof(ResetFreeze));
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(1.5f); // Reset speed after 2 seconds
        currentSpeed = speed; // Restore original speed
    }

    IEnumerator ResetFreeze()
    {
        yield return new WaitForSeconds(1f); // Reset speed after 2 seconds
        Slowed();
    }

    public virtual void Die()
    {
        currentSpeed = 0;
        PlayerStats.enemiesKilled++;
        Destroy(gameObject);
    }
}
