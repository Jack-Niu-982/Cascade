using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public float AttackInterval = 0.4f;
    static public float Damage;
    static public float PlayerHP = 50;
    static public float PlayerMP = 100;
    static public bool PerfectDefendCheck = false;
    static public float PDefendTime = 0.35f;
    static public float CD1 = 3f, CD2 = 5f;
    static public float SkillPass1 = 3, SkillPass2 = 5;

    public PlayerAnimation Playeranimation;
    public Rigidbody2D rb;
    public Transform groundCheckPoint;
    public LayerMask floorLayer;
    public float MoveSpeed = 10f;
    public float JumpHeight = 10f;
    public GameObject AttackCollider;
    public Animator animator;
    public GameObject FireSkill;
    public GameObject WaterSkill;

    float groundCheckRadius = .5f;
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
        if (Input.GetKeyDown(KeyCode.I) && SkillPass1 >= CD1 && PlayerMP >= 10)
        {
            PlayerMP -= 10;
            float facingDirection = transform.localScale.x < 0 ? 1f : -1f;
            var bfb = Instantiate(FireSkill, gameObject.transform.localPosition, gameObject.transform.localRotation);
            bfb.GetComponent<FireSkill>().SetDirection(-facingDirection);
            SkillPass1 = 0;
        }
        if(Input.GetKeyDown(KeyCode.O) && SkillPass2 >= CD2 && PlayerMP >= 20)
        {
            PlayerMP -= 20;
            float facingDirection = transform.localScale.x < 0 ? 1f : -1f;
            Vector3 startOffset = new Vector3(2f * facingDirection, 0f, 0f);
            Vector3 startPoint = transform.position + startOffset;
            Instantiate(WaterSkill, startPoint, Quaternion.identity);
            SkillPass2 = 0f;
        }
        if (animator.GetBool("IfAttacking") || animator.GetBool("IfDefending") && nextVelocityY >= 0)
        {
            nextVelocityX *= 0.5f;
            nextVelocityY = 0;
        }
            
        rb.velocity = new Vector2(nextVelocityX, nextVelocityY);
        if(!animator.GetBool("IfAttacking"))AttackCollider.SetActive(animator.GetBool("IfAttacking"));

        if (SkillPass1 < CD1)
            SkillPass1 += Time.deltaTime;
        if (SkillPass2 < CD2)
            SkillPass2 += Time.deltaTime;
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
    }
    public void SetAttackCollider()
    {
        AttackCollider.SetActive(true);
    }
}
