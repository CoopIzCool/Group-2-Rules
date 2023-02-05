using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Fields
    private float _runSpeed = 5f;
    private float _jumpPower = 20f;
    private float _horizontalInput;
    private bool _facingRight;

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask hazardLayer;
    #endregion Fields

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
        }

        if((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if(_horizontalInput > 0 && !_facingRight)
        {
            FlipSides();
        }
        else if(_horizontalInput < 0 && _facingRight)
        {
            FlipSides();
        }

        if(HitHazard())
        {
            PlayerDeath();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_horizontalInput * _runSpeed, rb.velocity.y);
    }

    private void FlipSides()
    {
        _facingRight = !_facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1.0f,
            transform.localScale.y, transform.localScale.z);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool HitHazard()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, hazardLayer);
    }

    private void PlayerDeath()
    {
        LevelManager.Instance.RestartLevel();
    }
}
