using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] List<float> puddleTimers = new List<float>();

    void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.9372549020f, 0.4117647059f, 0.3686274510f, 1f);
        StartCoroutine(RemoveWall());
    }

    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
                puddleTimers.RemoveAt(i);
                i--; // Adjust index after removal
            }
            puddleTimers[i] += Time.deltaTime;
            if (puddleTimers[i] > 0.3f)
            {
                enemies[i].GetComponent<Enemy>()?.TakeDamage(1f);
                puddleTimers[i] = 0f; // Reset timer after damage
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return; // Ignore enemies and enemy projectiles
        }

        enemies.Add(collision.gameObject);
        puddleTimers.Add(0f); // Initialize timer for the new enemy
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        int index = enemies.IndexOf(collision.gameObject);
        if (index != -1)
        {
            enemies.RemoveAt(index);
            puddleTimers.RemoveAt(index);
        }
    }

    IEnumerator RemoveWall()
    {
        yield return new WaitForSeconds(5f);
        enemies.Clear();
        puddleTimers.Clear();
        Destroy(gameObject);
    }
}
