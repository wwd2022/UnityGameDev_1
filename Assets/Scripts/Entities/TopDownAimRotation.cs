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
        _controller.OnLookEvent += OnAim; // 마우스 움직임에 구독을 걸었다
    }

    public void OnAim(Vector2 newAimDirection) // 마우스 움직일때
    {
        RotateArm(newAimDirection);
    }
    
    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 라디안을 도(각도)로 변환

        armRenderer.flipY = Mathf.Abs(rotZ) > 90f; // 무기 상하 반전
        characterRenderer.flipX = armRenderer.flipY; // 캐릭터 좌우 반전
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ); // 무기 회전
    }
}
