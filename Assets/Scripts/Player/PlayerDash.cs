using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDash : MonoBehaviour
{
    PlayerMovement playerMovement;
    Rigidbody2D rb;
    public float dashSpeed;
    public float dashLength;
    public float dashCooldown;
    public bool canDash;
    public bool isDashing;
    Vector2 movement;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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

    void Dash(){
        canDash = false;
        isDashing = true;
        playerMovement.canMove = false;
        rb.velocity = movement.normalized * dashSpeed;
        Invoke("StopDash", dashLength);
        Invoke("DashCooldown", dashCooldown);
    }

    void StopDash(){
        rb.velocity = Vector2.zero;
        playerMovement.canMove = true;
        isDashing = false;
    }

    void DashCooldown(){
        canDash = true;
    }
}
