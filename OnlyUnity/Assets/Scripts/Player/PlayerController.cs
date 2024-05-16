using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _SprintSpeed;
    [SerializeField] private float _airSpeed;

    [Header("Jump Force")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity = 9.81f;

    [Header("Mouse Sensitivity")]
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _UpAndDown = 80;


    private float MoveSpeed
    {
        get
        {
            if (_collision.IsGrounded)
            {
                return _playerActionManager.SprintValue > 0 ? _SprintSpeed : _walkSpeed;
            }
            else
                return _airSpeed;
            
        }
    }

    private bool _isMoving;

    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
            _anim.SetBool("IsMoving", value);
        }
    }
    private bool _isSprinting;

    public bool isSprinting
    {
        get => _isSprinting;
        set
        {
            _isSprinting = value;
            _anim.SetBool("IsSprinting", value);
        }
    }




    private CharacterController _controller;
    private PlayerActionManager _playerActionManager;
    public Camera _camera;
    private Animator _anim;
    private CollisionDetection _collision;

    private Vector3 _movement;
    private float _ver;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerActionManager = PlayerActionManager.Instance;
       // _camera = GameObject.Find("PlayerCamera").;
        _anim = GetComponent<Animator>();
        _collision = GetComponent<CollisionDetection>();
    }
    public void HandleUpdate()
    {
        HandleMove();
        HandleJump();
        HandleLook();
    }
    private void HandleMove()
    {
        float speed = MoveSpeed;

        Vector3 moveInput = new Vector3(_playerActionManager.MoveInput.x, 0, _playerActionManager.MoveInput.y);
        Vector3 inputToWorld = transform.TransformDirection(moveInput);
        inputToWorld.Normalize();

        _movement.x = inputToWorld.x * speed;
        _movement.z = inputToWorld.z * speed;

        _controller.Move(_movement * Time.deltaTime);

        //Moving or not moving
        IsMoving = moveInput.sqrMagnitude != 0;

        //Check if isSprinting
        isSprinting = _playerActionManager.SprintValue > 0;
    }
    private void HandleJump()
    {
        if (_collision.IsGrounded)
        {
            _movement.y = -0.5f;
            if (_playerActionManager.JumpTriggered)
            {
                _movement.y = _jumpForce;
            }
        }
        else
            _movement.y -= _gravity * Time.deltaTime;
    }
    private void HandleLook()
    {
        //Horizontal
        float hor = _playerActionManager.LookInput.x * _mouseSensitivity;
        transform.Rotate(0, hor, 0);

        //Vertical
        _ver -= _playerActionManager.LookInput.y * _mouseSensitivity;
        _ver = Mathf.Clamp(_ver, -_UpAndDown, _UpAndDown);
        _camera.transform.localRotation = Quaternion.Euler(_ver, 0, 0);
    }
}
