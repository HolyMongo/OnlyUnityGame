using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform player;
    private Vector2 _mouseVector;
    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        float mouseX = _mouseVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = _mouseVector.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
    void OnLook(InputValue MouseValue)
    {
        _mouseVector = MouseValue.Get<Vector2>();
    }
}
