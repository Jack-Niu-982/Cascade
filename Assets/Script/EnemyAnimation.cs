using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    public EnemyController controller;
    public float AttackActionTime;
    public float HurtActionTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<EnemyController>();
    }

    void Update()
    {
        animator.SetFloat("XSpeed", Mathf.Abs(controller.nextVelocityX));
        animator.SetFloat("YSpeed", controller.nextVelocityY);
        animator.SetBool("Grounded", controller.CheckGrounded());

        if (Time.time - AttackActionTime >= controller.AttackInterval)
        {
            animator.SetBool("IfAttacking", false);
        }
        if (Time.time - HurtActionTime >= controller.HurtTime)
        {
            animator.SetBool("IfHurt", false);
        }
    }

    public void SetAttackAnimation()
    {
        animator.SetBool("IfAttacking", true);
        AttackActionTime = Time.time;
    }

    public void SetDefendAnimation()
    {
        animator.SetBool("IfDefending", true);
    }

    public void ResetDefendAnimation()
    {
        animator.SetBool("IfDefending", false);
    }
    public void SetHurtAnimation()
    {
        animator.SetBool("IfHurt", true);
        HurtActionTime = Time.time;
    }
    public void SetDeathAnimation()
    {
        animator.SetBool("IfDead", true);
        controller.enabled = false;
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
