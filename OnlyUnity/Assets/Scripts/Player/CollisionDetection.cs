using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isOnWall;

    public bool IsGrounded
    {
        get => _isGrounded;
        set
        {
            _isGrounded = value;
            _anim.SetBool("IsGrounded", value);
        }
    }
    public bool IsOnWall
    {
        get => _isOnWall;
        set
        {
            _isOnWall = value;
            _anim.SetBool("IsOnWall", value);
        }
    }

    private Animator _anim;
    private CharacterController _controller;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
       IsGrounded = _controller.isGrounded;
       // IsOnWall = _controller.Raycast()
    }

}
