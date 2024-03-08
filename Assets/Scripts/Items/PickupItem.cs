using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    [SerializeField] private bool destroyOnPickup = true; // 픽업시 삭제여부
    [SerializeField] private LayerMask canBePickupBy; // 픽업가능 레이어
    [SerializeField] private AudioClip pickupSound; // 픽업 사운드

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer))) // 픽업가능시
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
