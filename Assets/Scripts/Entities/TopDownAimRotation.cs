using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    [SerializeField] private SpriteRenderer characterRenderer;

    private TopDownCharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _controller.OnLookEvent += OnAim; // ���콺 �����ӿ� ������ �ɾ���
    }

    public void OnAim(Vector2 newAimDirection) // ���콺 �����϶�
    {
        RotateArm(newAimDirection);
    }
    
    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ������ ��(����)�� ��ȯ

        armRenderer.flipY = Mathf.Abs(rotZ) > 90f; // ���� ���� ����
        characterRenderer.flipX = armRenderer.flipY; // ĳ���� �¿� ����
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ); // ���� ȸ��
    }
}
