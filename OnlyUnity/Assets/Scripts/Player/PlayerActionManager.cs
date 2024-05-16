using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset _inputAsset;

    [Header("Input Action Map")]
    [SerializeField] private string _inputMap = "Player";

    [Header("Input Action")]
    [SerializeField] private string _move = "Move";
    [SerializeField] private string _look = "Look";
    [SerializeField] private string _jump = "Jump";
    [SerializeField] private string _sprint = "Sprint";

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;


    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public float SprintValue { get; private set; }


    public static PlayerActionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        InitializeActions();
        InitializeActionEvents();
    }
    private void InitializeActions()
    {
        _moveAction = _inputAsset.FindActionMap(_inputMap).FindAction(_move);
        _lookAction = _inputAsset.FindActionMap(_inputMap).FindAction(_look);
        _jumpAction = _inputAsset.FindActionMap(_inputMap).FindAction(_jump);
        _sprintAction = _inputAsset.FindActionMap(_inputMap).FindAction(_sprint);
    }
    private void InitializeActionEvents()
    {
        _moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        _moveAction.canceled += _ => MoveInput = Vector2.zero;

        _lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        _lookAction.canceled += _ => LookInput = Vector2.zero;

        _jumpAction.performed += _ => JumpTriggered = true;
        _jumpAction.canceled += _ => JumpTriggered = false;

        _sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        _sprintAction.canceled += _ => SprintValue = 0;
    }
    private void OnEnable()
    {
        _inputAsset.FindActionMap(_inputMap).Enable();
    }
    private void OnDisable()
    {
        _inputAsset.FindActionMap(_inputMap).Disable();
    }
}
