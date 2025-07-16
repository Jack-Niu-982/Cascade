using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public float AttackInterval = 0.4f;
    static public float Damage;
    static public float PlayerHP = 10;
    static public float PlayerMP = 10;
    static public bool PerfectDefendCheck = false;
    static public float PDefendTime = 0.35f;

    public PlayerAnimation Playeranimation;
    public Rigidbody2D rb;
    public Transform groundCheckPoint;
    public LayerMask floorLayer;
    public float MoveSpeed = 10f;
    public float JumpHeight = 10f;
    public GameObject AttackCollider;
    public Animator animator;

    float groundCheckRadius = .2f;
    public int maxJumps = 1;
    public int jumpsRemaining = 0;
    public float horizontalInput;
    public float nextVelocityX;
    public float nextVelocityY;
    public float HurtTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Playeranimation = GetComponent<PlayerAnimation>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHP <= 0)
        {
            Playeranimation.SetDeathAnimation();
        }
        if (animator.GetBool("IfHurt"))
        {
            Debug.Log("GetHurting");
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
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
        if (!animator.GetBool("IfAttacking"))
        {
            if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }else if(horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.J)&&CheckGrounded() && !animator.GetBool("IfDefending") && !animator.GetBool("IfAttacking"))
        {
            Playeranimation.SetAttackAnimation();
        }
        if (Input.GetKeyDown(KeyCode.Semicolon) && CheckGrounded() && !animator.GetBool("IfAttacking"))
        {
            Playeranimation.SetDefendAnimation();
        }
        if (Input.GetKeyUp(KeyCode.Semicolon))
        {
            Playeranimation.ResetDefendAnimation();
        }
        if (animator.GetBool("IfAttacking") || animator.GetBool("IfDefending") && nextVelocityY >= 0)
        {
            nextVelocityX *= 0.5f;
            nextVelocityY = 0;
        }
            
        rb.velocity = new Vector2(nextVelocityX, nextVelocityY);
        if(!animator.GetBool("IfAttacking"))AttackCollider.SetActive(animator.GetBool("IfAttacking"));
    }

    public bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, floorLayer);
    }
    
    public void Hurt(float Damage, float hurtTime, GameObject Attacker)
    {
        float totD = Damage;
        HurtTime = hurtTime;
        
        if (PerfectDefendCheck)
        {
            totD = 0;
            if (Attacker.CompareTag("Enemy"))
            {
                EnemyController AttackerController = Attacker.GetComponent<EnemyController>();
                AttackerController.animator.SetBool("IfAttacking", false);
                AttackerController.Hurt(3, 0.5f);
            }
            else
            {
                BossController AttackerController = Attacker.GetComponent<BossController>();
                AttackerController.animator.SetBool("IfAttacking", false);
                AttackerController.Hurt(3, 1f);
            }
            CameraShake.Instance.Shake(2);
        }
        else if (animator.GetBool("IfDefending"))
        {
            totD *= 0.2f;
            CameraShake.Instance.Shake(0.2f);
        }
        else
        {
            Playeranimation.SetHurtAnimation();
        }
        PlayerHP -= totD;
        Debug.Log(PlayerHP);
    }
    public void SetAttackCollider()
    {
        AttackCollider.SetActive(true);
    }
}
