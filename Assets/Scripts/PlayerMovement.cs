using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpaceTimePositionChecker))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpaceTimePositionChecker _positionChecker;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _velocityPower;
    [SerializeField] private float _frictionAmount;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _fallGravityMultiplier;
    private Animator _animator;
    private bool _isJumping;
    

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _positionChecker = GetComponent<SpaceTimePositionChecker>();
        _animator = GetComponent<Animator>();
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

        if (_positionChecker.IsGrounded && Mathf.Abs(_frictionAmount) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rigidBody.velocity.x), Mathf.Abs(_frictionAmount));
            amount *= Mathf.Sign(_rigidBody.velocity.x);
            _rigidBody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (_positionChecker.IsGrounded && Mathf.Abs(_rigidBody.velocity.y) < 0.02)
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

        const string runningKey = "Running";
        const string jumpingKey = "Jumping";
        const string fallingKey = "Falling";

        if (_rigidBody.velocity.x > 1)
        {
            _animator.SetFloat(runningKey, 1);
            transform.localScale = Vector2.one;
            transform.localScale = new Vector3(1, 1, 1);
        } else if (_rigidBody.velocity.x < -1)
        {
            _animator.SetFloat(runningKey, 1);
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            _animator.SetFloat(runningKey, 0);
        }

        if (_rigidBody.velocity.y > 0.01f)
        {
            _animator.SetBool(jumpingKey, true);
        } else if (_rigidBody.velocity.y < -0.01f)
        {
            _animator.SetBool(fallingKey, true);
            _animator.SetBool(jumpingKey, false);
        } else
        {
            _animator.SetBool(fallingKey, false);
        }
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _isJumping = true;
    }
}
