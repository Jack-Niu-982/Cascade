using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyAnimation enemyAnimation;
    public Rigidbody2D rb;
    public Transform groundCheckPoint;
    public LayerMask floorLayer;
    public GameObject attackCollider;
    public Animator animator;
    public GameObject player;
    public float HP = 10;
    public float AttackInterval = 1f;

    public float moveSpeed = 10f;
    public float groundCheckRadius = 0.2f;
    public float horizontalInput;
    public float nextVelocityX;
    public float nextVelocityY;
    public float KnockTime;

    public float actionCooldown = 2f;
    public float lastActionTime = 0f;
    public float HurtTime = 0.5f;

    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (HP <= 0)
        {
            attackCollider.SetActive(false);
            rb.velocity = Vector3.zero;
            enemyAnimation.SetDeathAnimation();
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

        transform.localScale = new Vector3(directionToPlayer, 1, 1);

        if ((animator.GetBool("IfAttacking") || animator.GetBool("IfDefending")) && nextVelocityY >= 0)
        {
            nextVelocityX *= 0.5f;
            nextVelocityY = 0;
        }

        rb.velocity = new Vector2(nextVelocityX, nextVelocityY);
        if (!animator.GetBool("IfAttacking")) attackCollider.SetActive(animator.GetBool("IfAttacking"));
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
            if (rand <= 0.7f)
            {
                if (animator.GetBool("IfDefending"))
                {
                    enemyAnimation.ResetDefendAnimation();
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
        HurtTime = hurtTime;
        if (animator.GetBool("IfDefending"))
        {
            totD *= 0.2f;
        }
        else
        {
            enemyAnimation.SetHurtAnimation();
        }
        HP -= totD;
    }
    public void SetAttackCollider()
    {
        attackCollider.SetActive(true);
    }
}
