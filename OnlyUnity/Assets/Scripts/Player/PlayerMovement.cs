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

    [Header("Mouse things")]
    private Vector2 _mouseVector;
    public float MouseSpeed;

    void Start()
    {
        _rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rB.AddForce(_movementVector.x * Speed, 0.0f, _movementVector.y * Speed);
        Camera.main.transform.Rotate(-_mouseVector.y * MouseSpeed, _mouseVector.x * MouseSpeed, 0.0f);
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
    void OnLook(InputValue MouseValue)
    {
        _mouseVector = MouseValue.Get<Vector2>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * 0.01f, groundDistance);
    }
}
