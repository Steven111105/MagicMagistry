using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float playerSpeed;
    public float currentSpeed;
    public bool canMove;
    Vector2 movementInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
            rb.velocity = movementInput.normalized * playerSpeed;
        }
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.localEulerAngles = new Vector3(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);

        // Debug.Log(transform.up);
    }
}
