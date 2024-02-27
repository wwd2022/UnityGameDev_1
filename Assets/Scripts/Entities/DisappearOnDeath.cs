using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnDeath : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthSystem.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>()) // 나를포함한 내 하위에 모든 SpriteRenderer를 순회한다
        {
            Color color = renderer.color;
            color.a = 0.3f; // 약간 투명하게 만든다
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>()) // 나를포함한 내 하위에 모든 Behaviour를 순회한다
        {
            component.enabled = false; // 동작하지않도록 비활성화
        }

        Destroy(gameObject, 2f); // 게임오브젝트 삭제
    }
}