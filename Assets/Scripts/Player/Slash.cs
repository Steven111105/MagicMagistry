using System.Collections;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float damage = 1f;
    void OnEnable()
    {
        StartCoroutine(RemoveSlash());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Enemy>()?.TakeDamage(damage, transform,10f);
        collision.gameObject.GetComponent<Projectile>()?.Reflect(transform.localEulerAngles.z);
    }

    IEnumerator RemoveSlash()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
