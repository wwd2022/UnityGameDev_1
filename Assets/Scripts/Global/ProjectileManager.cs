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

    public void CreateImpactParticlesAtPostion(Vector3 position, RangedAttackData attackData) // �� ��ġ�� ��ƼŬ�� ��ڴ�
    {
        _impactParticleSystem.transform.position = position; // ��ƼŬ ��ġ�� �ٲ۴�
        ParticleSystem.EmissionModule em = _impactParticleSystem.emission; // ��ƼŬ�� �ֹ̼�(�����ϴ°�)�� �����´�
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(attackData.size * 5)));
        ParticleSystem.MainModule mainModule = _impactParticleSystem.main;
        mainModule.startSpeedMultiplier = attackData.size * 10f;
        _impactParticleSystem.Play(); // ��ƼŬ �÷���
    }
}
