using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("XSpeed", Mathf.Abs(controller.nextVelocityX));
        animator.SetFloat("YSpeed", controller.nextVelocityY);
        animator.SetBool("Grounded", controller.CheckGrounded());
    }
    public void SetAttackAnimation()
    {
        animator.SetBool("IfAttacking", true);
    }
}
