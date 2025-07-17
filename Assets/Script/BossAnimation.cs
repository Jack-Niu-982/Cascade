using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    public Animator animator;
    public BossController controller;
    public float AttackActionTime;
    public float HurtActionTime;
    public AudioSource AudioSource;
    public AudioClip AttackAudio;
    public AudioClip Attack1Audio;
    public AudioClip WalkAudio;
    public AudioClip DeathAudio;
    public AudioClip HurtAudio;
    public AudioClip WeaponAudio;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<BossController>();
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        animator.SetFloat("XSpeed", Mathf.Abs(controller.nextVelocityX));
        animator.SetFloat("YSpeed", controller.nextVelocityY);
        animator.SetBool("Grounded", controller.CheckGrounded());

        if (animator.GetBool("DecideAttack"))
        {
            if (Time.time - AttackActionTime >= controller.Attack1Interval)
            {
                animator.SetBool("IfAttacking", false);
            }
        }
        else
        {
            if (Time.time - AttackActionTime >= controller.Attack2Interval)
            {
                animator.SetBool("IfAttacking", false);
            }
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
        if (animator.GetBool("DecideAttack")) AudioSource.PlayOneShot(AttackAudio);
        if (!animator.GetBool("DecideAttack")) AudioSource.PlayOneShot(Attack1Audio);
    }

    public void SetDefendAnimation()
    {
        animator.SetBool("IfDefending", true);
        AudioSource.PlayOneShot(AttackAudio);
    }

    public void ResetDefendAnimation()
    {
        animator.SetBool("IfDefending", false);
    }
    public void SetHurtAnimation()
    {
        Debug.Log("BossGetHurt");
        animator.SetBool("IfAttacking", false);
        if(!animator.GetBool("IfHurt"))animator.SetBool("IfHurt", true);
        else
        {
            animator.SetBool("IfHurt", false);
            animator.SetBool("IfHurt", true);
        }
        HurtActionTime = Time.time;
        AudioSource.PlayOneShot(HurtAudio);
    }
    public void SetDeathAnimation()
    {
        animator.SetBool("IfDead", true);
        controller.enabled = false;
        AudioSource.PlayOneShot(DeathAudio);
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
    public void PlayWalkAudio()
    {
        AudioSource.PlayOneShot(WalkAudio);
    }
    public void PlayWeaponAudio()
    {
        AudioSource.PlayOneShot(WeaponAudio);
    }
}
