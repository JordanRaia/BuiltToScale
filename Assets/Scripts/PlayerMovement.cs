using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float baseSpeed = 8f;
    private float jumpingPower = 16f; //base
    private bool isFacingRight = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem moveParticles;

    private PlayerSizeController sizeController;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sizeController = GetComponent<PlayerSizeController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            float currentScale = sizeController.GetCurrentScale();
            float adjustedJumpPower = jumpingPower / (1 + 0.5f * (currentScale - 1));

            rb.velocity = new Vector2(rb.velocity.x, adjustedJumpPower);
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        HandleMovementParticles();
    }

    private void HandleMovementParticles()
    {
        // Check if the player is moving and grounded
        if (Math.Abs(rb.velocity.x) > 0.1 && IsGrounded())
        {
            if (!moveParticles.isPlaying)
                moveParticles.Play();
        }
        else
        {
            if (moveParticles.isPlaying)
                moveParticles.Stop();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
    }

    private void FixedUpdate()
    {
        //adjust speed based on the player's size
        float playerScale = transform.localScale.x;
        float speed = baseSpeed / playerScale;

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        // Set 'isJumping' to false if the player is grounded
        if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            // Instead of directly modifying the scale, rotate the character on the Y axis
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
