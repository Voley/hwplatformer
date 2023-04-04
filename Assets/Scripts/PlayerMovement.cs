using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _velocityPower;
    [SerializeField] private float _frictionAmount;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _fallGravityMultiplier;

    private Rigidbody2D _rigidBody;
    private ColliderDetector _groundDetector;
    private Animator _animator;
    private int animationRunningHash;
    private int animationJumpingHash;
    private int animationFallingHash;

    private const string animationRunningKey = "Running";
    private const string animationJumpingKey = "Jumping";
    private const string animationFallingKey = "Falling";

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponentInChildren<ColliderDetector>();
        _animator = GetComponent<Animator>();

        animationRunningHash = Animator.StringToHash(animationRunningKey);
        animationJumpingHash = Animator.StringToHash(animationJumpingKey);
        animationFallingHash = Animator.StringToHash(animationFallingKey);
    }

    void Update()
    {
        _frictionAmount = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        float targetSpeed = _frictionAmount * _moveSpeed;
        float speedDif = targetSpeed - _rigidBody.velocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelerationRate, _velocityPower) * Mathf.Sign(speedDif);
        _rigidBody.AddForce(movement * Vector2.right);

        UpdateAnimations();

        if (_groundDetector.IsColliding && Mathf.Abs(_frictionAmount) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rigidBody.velocity.x), Mathf.Abs(_frictionAmount));
            amount *= Mathf.Sign(_rigidBody.velocity.x);
            _rigidBody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        float velocityThreshold = 0.02f;

        if (Input.GetKey(KeyCode.Space))
        {
            if (_groundDetector.IsColliding && Mathf.Abs(_rigidBody.velocity.y) < velocityThreshold)
            {
                Jump();
            }
        }

        if (_rigidBody.velocity.y < 0)
        {
            _rigidBody.gravityScale = _gravityScale * _fallGravityMultiplier;
        } else
        {
            _rigidBody.gravityScale = _gravityScale;
        }
    }

    private void UpdateAnimations()
    {
        if (_rigidBody.velocity.x > 1)
        {
            _animator.SetFloat(animationRunningHash, 1);
            transform.localScale = Vector2.one;
            transform.localScale = new Vector3(1, 1, 1);
        } else if (_rigidBody.velocity.x < -1)
        {
            _animator.SetFloat(animationRunningHash, 1);
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            _animator.SetFloat(animationRunningHash, 0);
        }

        if (_rigidBody.velocity.y > 0.01f)
        {
            _animator.SetBool(animationJumpingHash, true);
        } else if (_rigidBody.velocity.y < -0.01f)
        {
            _animator.SetBool(animationFallingHash, true);
            _animator.SetBool(animationJumpingHash, false);
        } else
        {
            _animator.SetBool(animationFallingHash, false);
        }
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
