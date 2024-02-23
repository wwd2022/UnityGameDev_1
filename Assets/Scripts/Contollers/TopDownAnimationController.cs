using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class TopDownAnimationController : TopDownAnimations
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");
    // StringToHash("") 특정문자열을 해쉬값으로 변환한다
    // 문자열 연산은 비용이 크다, 해쉬값으로 변환하여 연산한다

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > .5f);
    }

    private void Attacking(AttackSO obj)
    {
        animator.SetTrigger(Attack);
    }

    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }

    private void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}
