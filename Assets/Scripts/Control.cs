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

    private bool facingRight = true;
    private Rigidbody2D rb;
    private int currentJumpCount, maxJumpCount = 1;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        // positive: right, negative: left
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * moveSpeed * horizontalInput * Time.deltaTime);

        // flip sprite according to direction
        if (horizontalInput != 0) 
        {
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
    }

    private Regex rg = new Regex(@"^[1-6]"); // numbers from 1 to 6
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (currentJumpCount < maxJumpCount)
            {
                rb.velocity = gravity.Direction * -jumpSpeed;
                currentJumpCount++;
                JumpCountText.text = "JumpCount: " + currentJumpCount;
                if(currentJumpCount == maxJumpCount) JumpCountText.text = "JumpCount: " + currentJumpCount + " (max)";
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
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            currentJumpCount = 0;
            JumpCountText.text = "JumpCount: " + currentJumpCount;
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

        JumpCountText.text = "JumpCount: " + currentJumpCount;
    }
}