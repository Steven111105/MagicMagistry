using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(RemoveSlash());
    }

    IEnumerator RemoveSlash()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
