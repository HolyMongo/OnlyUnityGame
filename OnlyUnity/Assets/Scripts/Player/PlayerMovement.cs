using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    public float Speed;
    public float JumpSpeed;

    [Header("Also Speed")]
    private Vector2 _movementVector;
    private Rigidbody _rB;

    [Header("Jump Things")]
    public float groundDistance = 0.4f;
    public LayerMask ground;
    private Transform _cameraTransform;
    private void Awake()
    {
    }
    void Start()
    {
        _rB = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // transform.Translate(_movementVector.x * Speed * Time.deltaTime, 0.0f, _movementVector.y * Speed * Time.deltaTime, Space.Self);
        //  _rB.AddForce(_movementVector.x *  Speed, 0.0f, _movementVector.y * Speed);


        Vector3 movementDirection = _cameraTransform.forward * _movementVector.y + _cameraTransform.right * _movementVector.x;
        movementDirection.y = 0f; // Ensure movement is only in the horizontal plane
        movementDirection.Normalize(); // Normalize to ensure consistent movement speed

        // Apply force in the direction of movement
        _rB.AddForce(movementDirection * Speed);
    }
    void OnMove(InputValue movementValue)
    {
        _movementVector = movementValue.Get<Vector2>();
    }
    void OnJump()
    {
        if(Physics.CheckSphere(transform.position, groundDistance, ground))
        {
            _rB.AddForce(0f, JumpSpeed, 0f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * 0.01f, groundDistance);
    }
}
