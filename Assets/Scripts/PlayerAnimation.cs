using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        /*if (_rigidBody.velocity.x > 0)
        {
            _animator.SetInteger("Running", 1);
        } else if (_rigidBody.velocity.x < 0)
        {
            _animator.SetInteger("Running", -1);
        } else
        {
            _animator.SetInteger("Running", 0);
        }*/
    }
}
