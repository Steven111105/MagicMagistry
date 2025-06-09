using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public void SpawnWall(float duration)
    {
        StartCoroutine(RemoveWall(duration));
    }

    IEnumerator RemoveWall(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
