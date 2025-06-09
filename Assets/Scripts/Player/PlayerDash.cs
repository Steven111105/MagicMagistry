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
    PlayerAnim playerAnim;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyProjectile"), false);
        playerAnim = transform.GetChild(0).GetComponent<PlayerAnim>();
    }

    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(canDash){
            if(movement != Vector2.zero && Input.GetKeyDown(KeyCode.Space)){
                // Debug.Log("Dashing");
                Dash();
            }
        }
    }

    void Dash()
    {
        canDash = false;
        isDashing = true;
        playerMovement.canMove = false;
        rb.velocity = movement.normalized * dashSpeed;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyProjectile"), true);
        Invoke("StopDash", dashLength);
        Invoke("DashCooldown", dashCooldown);
        playerAnim.TriggerDashAnimation();
    }

    void StopDash()
    {
        rb.velocity = Vector2.zero;
        playerMovement.canMove = true;
        isDashing = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyProjectile"), false);
        playerAnim.StopDashAnimation();
    }

    void DashCooldown(){
        canDash = true;
    }
}
