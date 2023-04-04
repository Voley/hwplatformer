using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ColliderDetector _frontDetector;
    [SerializeField] private ColliderDetector _frontGroundDetector;
    [SerializeField] private ColliderDetector _topDetector;
    [SerializeField] private float _moveSpeed;
    private Collider2D _collider;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private bool _isDead;
    private Vector2 _savedVelocity;

    public void CollectibleWasPickedUp()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        UpdateMovement();
    }

    private void FixedUpdate()
    {
        if (_isDead) return;

        if (_frontDetector.IsColliding || !_frontDetector.IsColliding && !_frontGroundDetector.IsColliding)
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            UpdateMovement();
        }

        if (_rigidBody.velocity != _savedVelocity)
        {
            _rigidBody.velocity = _savedVelocity;
        }

        if (_topDetector.IsColliding)
        {
            _animator.SetTrigger("Killed");
            _collider.isTrigger = true;
            _rigidBody.gravityScale = 0;
            _rigidBody.velocity = Vector3.zero;
            _isDead = true;
        }
    }

    private void UpdateMovement()
    {
        if (transform.localScale.x > Mathf.Epsilon)
        {
            _rigidBody.velocity = new Vector2(_moveSpeed, 0);
        }
        else
        {
            _rigidBody.velocity = new Vector2(-_moveSpeed, 0);
        }

        _savedVelocity = _rigidBody.velocity;
    }
}
