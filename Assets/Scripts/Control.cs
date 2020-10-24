using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float jumpSpeed = 10f;

    public static Gravity gravity;

    public Text JumpCountText;
    public Animator animator;
    public Collider2D foot_Collider;
    public CameraControl cameraControl;

    private bool facingRight = true;
    private Rigidbody2D rb;
    private int currentJumpCount, maxJumpCount = 1;
    private bool isDead = false;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            // positive: right, negative: left
            float horizontalInput = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.right * moveSpeed * horizontalInput * Time.deltaTime);

            // flip sprite according to direction
            if (horizontalInput != 0)
            {
                animator.SetBool("Is_Walking", true);
                if (horizontalInput > 0f && facingRight)
                {
                    Flip();
                    facingRight = false;
                }
                else if (horizontalInput < -0f && !facingRight)
                {
                    Flip();
                    facingRight = true;
                }
            }
            else
            {
                animator.SetBool("Is_Walking", false);
            }
        }
    }

    private Regex rg = new Regex(@"^[1-6]"); // numbers from 1 to 6
    private void Update()
    {
        if (!isDead)
        {
            if (currentJumpCount == 0)
            {
                if (Math.Abs(rb.velocity.y) >= 1)
                {
                    currentJumpCount++;
                    animator.SetBool("Is_Jumping", true);
                }
            }
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("Is_Jumping", true);
                if (currentJumpCount < maxJumpCount)
                {
                    rb.velocity = gravity.Direction * -jumpSpeed;
                    currentJumpCount++;
                    if (currentJumpCount == maxJumpCount) JumpCountText.text = "JumpCount: " + currentJumpCount + " (max)";
                }
            }

            if (Input.anyKeyDown)
            {
                string keyPressed = Input.inputString;
                if (rg.IsMatch(keyPressed))
                {
                    gravity.DirectionNum = int.Parse(keyPressed);
                    rb.rotation = gravity.DirectionDegree + 90;
                }
            }

            JumpCountText.text = "JumpCount: " + currentJumpCount;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            currentJumpCount = 0;
            JumpCountText.text = "JumpCount: " + currentJumpCount;
            animator.SetBool("Is_Jumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "spikes")
        {
            Debug.Log("spike collision");
            isDead = true;
            animator.SetTrigger("Is_Dead");
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1;
        transform.localScale = newLocalScale;
    }
    private void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = GetComponent<Gravity>();
        gravity.DirectionNum = 5;
        currentJumpCount = 0;
        animator.SetBool("Is_Walking", false);
        animator.SetBool("Is_Jumping", false);

        JumpCountText.text = "JumpCount: " + currentJumpCount;
    }
}