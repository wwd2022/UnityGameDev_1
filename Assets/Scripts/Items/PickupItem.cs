using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    [SerializeField] private bool destroyOnPickup = true; // �Ⱦ��� ��������
    [SerializeField] private LayerMask canBePickupBy; // �Ⱦ����� ���̾�
    [SerializeField] private AudioClip pickupSound; // �Ⱦ� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer))) // �Ⱦ����ɽ�
        {
            OnPickedUp(other.gameObject);
            if (pickupSound)
                SoundManager.PlayClip(pickupSound);

            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }
        }
    }

    protected abstract void OnPickedUp(GameObject receiver);
}
