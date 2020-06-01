﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public float jumpForce;
    public bool isJumping;
    public bool isGrounded;
    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;


    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);

        float horizontalmovement = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;

        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            isJumping = true;
        }

        movePlayer(horizontalmovement);

        Flip(rb.velocity.x);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("speed", characterVelocity);
    }

    void movePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping == true) 
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
    void Flip(float _velocity) {
        if (_velocity > 0.1f) {
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1f) {
            spriteRenderer.flipX = true;
        }

    }

}
