using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    private Vector2 _movementVector;
    private Rigidbody _rB;
 
    void Start()
    {
        _rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rB.AddForce(_movementVector.x * Speed, 0.0f, _movementVector.y * Speed);
    }
    void OnMove(InputValue movementValue)
    {
        _movementVector = movementValue.Get<Vector2>();
    }
}
