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

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>()) // ���������� �� ������ ��� SpriteRenderer�� ��ȸ�Ѵ�
        {
            Color color = renderer.color;
            color.a = 0.3f; // �ణ �����ϰ� �����
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>()) // ���������� �� ������ ��� Behaviour�� ��ȸ�Ѵ�
        {
            component.enabled = false; // ���������ʵ��� ��Ȱ��ȭ
        }

        Destroy(gameObject, 2f); // ���ӿ�����Ʈ ����
    }
}