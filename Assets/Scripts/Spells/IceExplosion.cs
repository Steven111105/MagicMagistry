using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosion : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(RemoveExplosion());
    }
    IEnumerator RemoveExplosion()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
