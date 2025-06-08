using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    public float damage = 5f;
    public int level;
    public float size = 1f; // Size of the fireball
    public GameObject explosionEffectPrefab;
    public void SetupSize(int size)
    {
        this.level = size;
        switch (level)
        {
            case 1:
                this.size = 1f;
                break;
            case 2:
                this.size = 1.5f;
                break;
            default:
                Debug.Log("Invalid size level for iceball: " + level);
                break;
        }
        transform.localScale = new Vector3(size, size, 1f);
    }
    private void OnDestroy()
    {
        //create a fire explosion effect when the fireball is destroyed using physics overlap circle
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, size+1);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // Assuming the enemy has a method to take damage
                hitCollider.GetComponent<Enemy>()?.TakeDamage(damage);
                hitCollider.GetComponent<Enemy>()?.Freeze();
            }
        }
        GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(size+1, size+1, 1);
    }
}
