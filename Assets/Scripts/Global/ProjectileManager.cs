using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _impactParticleSystem;

    public static ProjectileManager instance;

    private ObjectPool objectPool;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    public void ShootBullet(Vector2 startPostiion, Vector2 direction, RangedAttackData attackData)
    {
        GameObject obj = objectPool.SpawnFromPool(attackData.bulletNameTag);

        obj.transform.position = startPostiion;
        RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
        attackController.InitializeAttack(direction, attackData, this);

        obj.SetActive(true);
    }

    public void CreateImpactParticlesAtPostion(Vector3 position, RangedAttackData attackData) // 이 위치에 파티클을 찍겠다
    {
        _impactParticleSystem.transform.position = position; // 파티클 위치를 바꾼다
        ParticleSystem.EmissionModule em = _impactParticleSystem.emission; // 파티클의 애미션(생성하는거)를 가져온다
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(attackData.size * 5)));
        ParticleSystem.MainModule mainModule = _impactParticleSystem.main;
        mainModule.startSpeedMultiplier = attackData.size * 10f;
        _impactParticleSystem.Play(); // 파티클 플레이
    }
}
