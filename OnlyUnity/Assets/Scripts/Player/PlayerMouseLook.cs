using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 80f;
    public Transform player;
    private Vector2 _mouseVector;
  
    private float mouseY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Handle horizontal
        float mouseX = _mouseVector.x * mouseSensitivity;
        player.Rotate(0, mouseX, 0);

        //Handle Vertical
        mouseY -= _mouseVector.y * mouseSensitivity;
        mouseY = Mathf.Clamp(mouseY, -45f, 45f);
        transform.localRotation = Quaternion.Euler(mouseY, 0, 0);
      
    }
    void OnLook(InputValue MouseValue)
    {
        _mouseVector = MouseValue.Get<Vector2>();
    }
}
