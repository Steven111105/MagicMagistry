using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D rb;
    public float dashSpeed;
    public bool canDash;
    public bool isDashing;
    Vector2 movement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
    }

    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(canDash){
            if(movement != Vector2.zero && Input.GetKeyDown(KeyCode.Space)){
                Debug.Log("Dashing");
                Dash();
            }
        }
    }
}
