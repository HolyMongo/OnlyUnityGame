using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float Speed;
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

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 movementDirection = _cameraTransform.forward * _movementVector.y + _cameraTransform.right * _movementVector.x;
          movementDirection.y = 0f; // Ensure movement is only in the horizontal plane
          movementDirection.Normalize(); // Normalize to ensure consistent movement speed

        _controller.Move(movementDirection * Speed * Time.deltaTime);

        //if (movementDirection != Vector3.zero)
        //{
        //    gameObject.transform.forward = movementDirection;
        //}

        playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }
    void OnMove(InputValue movementValue)
    {
        _movementVector = movementValue.Get<Vector2>();
    }
    void OnJump()
    {
        if (Physics.CheckSphere(transform.position, groundDistance, ground))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * _gravityValue);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * 0.01f, groundDistance);
    }
}
//https://docs.unity3d.com/ScriptReference/CharacterController.Move.html