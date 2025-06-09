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
    PlayerAnim playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        playerAnim = transform.GetChild(0).GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
            rb.velocity = movementInput.normalized * playerSpeed;
            if (movementInput.x < 0)
            {
                playerAnim.isMovingLeft = true;
            }
            else if (movementInput.x > 0)
            {
                playerAnim.isMovingLeft = false;
            }

            if (movementInput == Vector2.zero)
            {
                playerAnim.isMoving = false;
            }
            else
            {
                playerAnim.isMoving = true;
            }
        }

        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.localEulerAngles = new Vector3(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);

        // Debug.Log(transform.up);
    }
}
