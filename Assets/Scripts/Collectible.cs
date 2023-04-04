using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    private Animator _animator;

    public void CollectibleWasPickedUp()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        float randomTime = Random.Range(0.0f, 1.0f);
        _animator.Play("Idle", 0, randomTime);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetTrigger("PickedUp");
    }
}
