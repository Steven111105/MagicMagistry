using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float playerSpeed;
    public float currentSpeed;
    public bool allowMovement;
    Vector2 movementInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        allowMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(allowMovement){
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
            rb.velocity = movementInput.normalized * playerSpeed;
        }
    }
}
