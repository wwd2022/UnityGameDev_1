using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimations : MonoBehaviour
{
    protected Animator animator;
    protected TopDownCharacterController controller;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // 자식 객체의 컴포넌트
        controller = GetComponent<TopDownCharacterController>(); // C# 스크립트 컴포넌트
    }
}
