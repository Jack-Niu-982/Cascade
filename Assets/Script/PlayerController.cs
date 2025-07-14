using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public float AttackInterval;
    static public float Damage;
    static public float PlayerHP;
    static public float PlayerMP;

    public PlayerAnimation PlayerAnimation;
    public Rigidbody2D rb;
    public Transform groundCheckPoint;
    public LayerMask floorLayer;
    public float MoveSpeed = 10f;
    public float JumpHeight = 10f;
    public GameObject AttackCollider;

    float groundCheckRadius = .2f;
    public int maxJumps = 2;
    public int jumpsRemaining = 0;
    public float horizontalInput;
    public float nextVelocityX;
    public float nextVelocityY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        nextVelocityX = horizontalInput * MoveSpeed;
        nextVelocityY = rb.velocity.y;
        if (CheckGrounded())
        {
            jumpsRemaining = maxJumps;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            jumpsRemaining -= 1;
            nextVelocityY = JumpHeight;
        }
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }else if(horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {

        }
        rb.velocity = new Vector2(nextVelocityX, nextVelocityY);
    }

    public bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, floorLayer);
    }
    
    public void Attack()
    {
        AttackCollider.SetActive(true);
    }
}
