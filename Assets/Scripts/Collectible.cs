using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    private Animator _animator;
    private const string animationIdleKey = "Idle";
    private const string animationPickupKey = "PickedUp";
    private int animationIdleHash;
    private int animationPickupHash;

    public void CollectibleWasPickedUp()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        animationIdleHash = Animator.StringToHash(animationIdleKey);
        animationPickupHash = Animator.StringToHash(animationPickupKey);
    }
    
    private void Start()
    {
        float randomTime = Random.Range(0.0f, 1.0f);
        _animator.Play(animationIdleHash, 0, randomTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetTrigger(animationPickupHash);
    }
}
