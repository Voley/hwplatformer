using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceTimePositionChecker : MonoBehaviour
{
    public bool IsGrounded => _groundDetector.IsGrounded;

    private GroundDetector _groundDetector;

    private void Awake()
    {
        _groundDetector = GetComponentInChildren<GroundDetector>();
    }
}
