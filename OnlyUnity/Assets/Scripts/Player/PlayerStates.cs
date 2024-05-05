using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking,
    Sprinting,
    Attacking,
    Dying
}
public class PlayerStates : MonoBehaviour
{
    public PlayerMovement player;
    PlayerState _state = PlayerState.Idle;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator.GetComponent<Animator>();
        player.StateHandler += Player_StateHandler;
    }

    public void CheatAnim(PlayerState state)
    {
        Player_StateHandler(state);
    }

    private void Player_StateHandler(PlayerState state)
    {
        _state = state;
        switch (state)
        {
            case PlayerState.Idle:
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsAttacking", false);
                _animator.SetBool("IsIdle", true);
                break;
            case PlayerState.Walking:
                _animator.SetBool("IsAttacking", false);
                _animator.SetBool("IsWalking", true);
                _animator.SetBool("IsSprinting", false);
                _animator.SetBool("IsIdle", false);
                break;
            case PlayerState.Sprinting:
                _animator.SetBool("IsAttacking", false);
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsSprinting", true);
                break;
            case PlayerState.Attacking:
                _animator.SetBool("IsAttacking", true);
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsSprinting", false);
                _animator.SetBool("IsIdle", false);
                //Attacking
                break;
            case PlayerState.Dying:
                Debug.Log("Dead");
                break;
            default:
                break;
        }
    }
}
