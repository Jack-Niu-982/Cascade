using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    public EnemyController controller;
    public float AttackActionTime;
    public float HurtActionTime;
    public AudioSource AudioSource;
    public AudioClip AttackAudio;
    public AudioClip DeathAudio;
    public AudioClip HurtAudio;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<EnemyController>();
        AudioSource = GetComponent<AudioSource>();
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
        AudioSource.PlayOneShot(AttackAudio);
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
        AudioSource.PlayOneShot(HurtAudio);
    }
    public void SetDeathAnimation()
    {
        animator.SetBool("IfDead", true);
        AudioSource.PlayOneShot(DeathAudio);
        controller.enabled = false;
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
