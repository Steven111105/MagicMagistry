using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject slashPrefab;
    public float slashCooldown = 0.5f;
    public float slashDamage;
    bool canSlash = true;
    public bool allowSlash = true;
    Vector2 slashDirection;
    // Start is called before the first frame update
    void Start()
    {
        allowSlash = true;
        canSlash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(allowSlash){
            if(canSlash && Input.GetMouseButton(0))
            {
                StartCoroutine(PerformSlash());
            }
        }
    }

    IEnumerator PerformSlash()
    {
        canSlash = false;
        slashDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        GameObject slash = Instantiate(slashPrefab, transform);
        slash.transform.localEulerAngles = new Vector3(0,0, Mathf.Atan2(slashDirection.y, slashDirection.x) * Mathf.Rad2Deg + 180f);
        yield return new WaitForSeconds(slashCooldown);
        canSlash = true;
    }
}
