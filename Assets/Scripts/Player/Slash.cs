using System.Collections;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float damage = 1f;
    [SerializeField] PlayerAttack playerAttack;
    void OnEnable()
    {
        StartCoroutine(RemoveSlash());
        playerAttack = transform.parent.GetComponent<PlayerAttack>();
        damage = playerAttack.slashDamage;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Enemy>()?.TakeDamage(damage, transform,10f);
        collision.gameObject.GetComponent<Projectile>()?.Reflect(transform.parent.up);
        if(collision.gameObject.CompareTag("EnemyProjectile"))
        {
            playerAttack.ReflectAudio();
        }
    }

    IEnumerator RemoveSlash()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
