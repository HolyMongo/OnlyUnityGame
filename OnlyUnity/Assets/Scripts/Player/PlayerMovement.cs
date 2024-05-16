using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float Speed;
    [SerializeField] private float sprintBonus = 2;
    private float sprintMultiplier = 1;
    private Vector2 _movementVector;
   

    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask ground;
    private bool _groundedPlayer;
    private float _gravityValue = -9.81f;
    private Vector3 playerVelocity;

    private Transform _cameraTransform;
    private CharacterController _controller;

    public event Action<PlayerState> StateHandler;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        StateHandler?.Invoke(PlayerState.Idle);
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        else if(_controller.isGrounded == false)
        {
            playerVelocity.y += _gravityValue * Time.deltaTime;
        }

        Vector3 movementDirection = transform.forward * _movementVector.y + transform.right * _movementVector.x;
         // movementDirection.y = 0f; // Ensure movement is only in the horizontal plane
          movementDirection.Normalize(); // Normalize to ensure consistent movement speed
        playerVelocity.x = movementDirection.x;
        playerVelocity.z = movementDirection.z;

        _controller.Move(playerVelocity * Speed * sprintMultiplier * Time.deltaTime);

        if (movementDirection == Vector3.zero)
        {
            StateHandler?.Invoke(PlayerState.Idle);
        }
       else if(sprintMultiplier == 1)
            StateHandler?.Invoke(PlayerState.Walking);
        else if (sprintMultiplier != 1)
            StateHandler?.Invoke(PlayerState.Sprinting);

        //else
        //    StateHandler?.Invoke(PlayerState.Walking);

      
       // _controller.Move(playerVelocity * Time.deltaTime);

        //if (Keyboard.current.spaceKey.isPressed)
        //    PlayerHealth.Instance.RecievedDamage(1);

    }
    void OnMove(InputValue movementValue)
    {
        _movementVector = movementValue.Get<Vector2>();
        StateHandler?.Invoke(PlayerState.Walking);
    }
    void OnJump()
    {
        if (Physics.CheckSphere(transform.position, groundDistance, ground))
        {
            //  playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * _gravityValue);
            playerVelocity.y = jumpHeight;
        }
    }
    void OnSprintActivate()
    {
        sprintMultiplier = sprintBonus;
    }
    void OnSprintCancel()
    {
        sprintMultiplier = 1;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * 0.01f, groundDistance);
    }
}
//https://docs.unity3d.com/ScriptReference/CharacterController.Move.html