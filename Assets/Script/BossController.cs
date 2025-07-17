using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossAnimation enemyAnimation;
    public Rigidbody2D rb;
    public Transform groundCheckPoint;
    public LayerMask floorLayer;
    public GameObject attackCollider1;
    public GameObject attackCollider2;
    public Animator animator;
    public GameObject player;
    public GameObject LevelTrigger;
    public float HP = 100;
    public float Attack1Interval = 2f;
    public float Attack2Interval = 1f;
    public float Attack1Poss = 0.5f;
    public float AttackPoss = 0.7f;

    public float moveSpeed = 10f;
    public float groundCheckRadius = 0.2f;
    public float horizontalInput;
    public float nextVelocityX;
    public float nextVelocityY;
    public float KnockTime;
    public bool IfForward;
    public float ForwardDir;

    public float actionCooldown = 2f;
    public float lastActionTime = 0f;
    public float HurtTime = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimation = GetComponent<BossAnimation>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (HP <= 0)
        {
            enemyAnimation.SetDeathAnimation();
            rb.velocity = Vector3.zero;
            attackCollider1.SetActive(false);
            attackCollider2.SetActive(false);
            LevelTrigger.SetActive(true);
            return;
        }
        if (KnockTime > 0)
        {
            KnockTime -= Time.deltaTime;
            Hurt(0, 0.2f);
        }
        if (animator.GetBool("IfHurt"))
        {
            return;
        }
        nextVelocityY = rb.velocity.y;

        if (Time.time - lastActionTime >= actionCooldown)
        {
            DecideNextAction();
            lastActionTime = Time.time;
        }

        float distanceX = player.transform.position.x - transform.position.x;
        float directionToPlayer = Mathf.Sign(distanceX);

        transform.localScale = new Vector3(-directionToPlayer, 1, 1);

        if ((animator.GetBool("IfAttacking") || animator.GetBool("IfDefending")) && nextVelocityY >= 0)
        {
            nextVelocityX *= 0.5f;
            nextVelocityY = 0;
        }
        if (IfForward&& animator.GetBool("IfAttacking"))
        {
            nextVelocityX += 10 * ForwardDir;
        }
        rb.velocity = new Vector2(nextVelocityX, nextVelocityY);
        if (!animator.GetBool("IfAttacking")) attackCollider1.SetActive(animator.GetBool("IfAttacking"));
        if (!animator.GetBool("IfAttacking")) attackCollider2.SetActive(animator.GetBool("IfAttacking"));
    }

    void DecideNextAction()
    {
        if (player == null)
        {
            horizontalInput = Random.Range(0f, 1f);
            nextVelocityX = Mathf.Clamp(horizontalInput * moveSpeed * Time.deltaTime, -5f, 5f);
            return;
        }

        float distanceX = player.transform.position.x - transform.position.x;
        float absDistanceX = Mathf.Abs(distanceX);
        float directionToPlayer = Mathf.Sign(distanceX);
        float rand = Random.value;

        if (absDistanceX <= 4f)
        {
            if (rand <= AttackPoss)
            {
                if (animator.GetBool("IfDefending"))
                {
                    enemyAnimation.ResetDefendAnimation();
                }
                if (rand <= Attack1Poss)
                {
                    animator.SetBool("DecideAttack", true);
                }
                else
                {
                    animator.SetBool("DecideAttack", false);
                }
                enemyAnimation.SetAttackAnimation();
                nextVelocityX = 0;
            }
            else if (rand <= 0.9f)
            {
                enemyAnimation.SetDefendAnimation();
                nextVelocityX = 0;
            }
            else if (rand <= 1f)
            {
                horizontalInput = directionToPlayer;
                enemyAnimation.ResetDefendAnimation();
            }
        }
        else
        {
            if (rand < 0.8f)
            {
                horizontalInput = directionToPlayer;
            }
            else
            {
                horizontalInput = -directionToPlayer;
            }
            enemyAnimation.ResetDefendAnimation();
        }

        nextVelocityX = Mathf.Clamp(horizontalInput * moveSpeed, -5f, 5f);
    }


    public bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, floorLayer);
    }

    public void Hurt(float damage, float hurtTime)
    {
        float totD = damage;
        if(hurtTime <= 0.2f)
        {
            HurtTime = 0.2f;
        }
        else
        {
            HurtTime = hurtTime * 2;
        }

        if (animator.GetBool("IfDefending"))
        {
            totD *= 0.2f;
        }else if (animator.GetBool("IfAttacking"))
        {
            totD = damage;
        }
        else
        {
            if(attackCollider2.activeSelf) attackCollider2.SetActive(!attackCollider2.activeSelf);
            if(attackCollider1.activeSelf) attackCollider1.SetActive(!attackCollider1.activeSelf);
            enemyAnimation.SetHurtAnimation();
        }
        HP -= totD;
    }
    public void SetAttackCollider1()
    {
        attackCollider1.SetActive(!attackCollider1.activeSelf);
    }
    public void SetAttackCollider2()
    {
        attackCollider2.SetActive(!attackCollider2.activeSelf);
    }
    public void GetForward()
    {
        float distanceX = player.transform.position.x - transform.position.x;
        float directionToPlayer = Mathf.Sign(distanceX);
        ForwardDir = directionToPlayer;
        IfForward = !IfForward;
        Debug.Log("ForwardGet");
    }
}
