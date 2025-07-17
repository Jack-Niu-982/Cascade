using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public PlayerController controller;
    public float AttackActionTime;
    public float PerfectDefendActionTime;
    public float HurtActionTime;
    public AudioSource AudioSource;
    public AudioClip AttackAudio;
    public AudioClip Attack1Audio;
    public AudioClip WalkAudio;
    public AudioClip DeathAudio;
    public AudioClip HurtAudio;
    public AudioClip Jump;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("XSpeed", Mathf.Abs(controller.nextVelocityX));
        animator.SetFloat("YSpeed", controller.nextVelocityY);
        animator.SetBool("Grounded", controller.CheckGrounded());
        if(Time.time - AttackActionTime >= PlayerController.AttackInterval)
        {
            animator.SetBool("IfAttacking", false);
        }
        if(Time.time - PerfectDefendActionTime >= PlayerController.PDefendTime)
        {
            PlayerController.PerfectDefendCheck = false;
        }
        if(Time.time - HurtActionTime >= controller.HurtTime)
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
        PerfectDefendActionTime = Time.time;
        PlayerController.PerfectDefendCheck = true;
        AudioSource.PlayOneShot(AttackAudio);
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
        controller.enabled = false;
        AudioSource.PlayOneShot(DeathAudio);
    }
    public void SelfDestroy()
    {
        GameManager.IfDie = true;
    }
    public void PlayWalkAudio()
    {
        AudioSource.PlayOneShot(WalkAudio);
    }
    public void PlayJumpAudio()
    {
        AudioSource.PlayOneShot(Jump);
    }
}
