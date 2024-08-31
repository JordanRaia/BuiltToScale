using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private bool moveLeft;
    private bool moveRight;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        sizeController = GetComponent<PlayerSizeController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust horizontal movement based on button presses
        if (moveLeft)
            horizontal = -1f;
        else if (moveRight)
            horizontal = 1f;
        else
            horizontal = 0f;

        if (jump && IsGrounded())
        {
            float currentScale = sizeController.GetCurrentScale();
            float adjustedJumpPower = jumpingPower / (1 + 0.5f * (currentScale - 1));

            rb.velocity = new Vector2(rb.velocity.x, adjustedJumpPower);
            animator.SetBool("isJumping", true);
            jump = false; // reset jump flag
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
        float playerScale = transform.localScale.x;
        float speed = baseSpeed / playerScale;

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

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
            transform.Rotate(0f, 180f, 0f);
        }
    }

    // UI Button methods
    public void OnMoveLeftPressed()
    {
        moveLeft = true;
    }

    public void OnMoveLeftReleased()
    {
        moveLeft = false;
    }

    public void OnMoveRightPressed()
    {
        moveRight = true;
    }

    public void OnMoveRightReleased()
    {
        moveRight = false;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}
