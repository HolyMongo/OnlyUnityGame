using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dying
}
public class EnemyStates : MonoBehaviour
{
    public Pathfinding pathfinding;
    EnemyState _state = EnemyState.Idle;
    public Animator animator;
    [SerializeField] private BaseEnemy baseEnemy;

    private void Awake()
    {
        pathfinding.StateHandler += Pathfinding_StateHandler;
        baseEnemy = transform.GetComponent<BaseEnemy>();
    }

    public void CheatAnim(EnemyState state)
    {
        Pathfinding_StateHandler(state);
    }

    private void Pathfinding_StateHandler(EnemyState state)
    {
        _state = state;
        switch (state)
        {
            case EnemyState.Idle:
                animator.SetBool("IsWalking", false);
                break;
            case EnemyState.Walking:
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsWalking", true);
                break;
            case EnemyState.Attacking:
                animator.SetBool("IsAttacking", true);
                baseEnemy.Attack();
                break;
            case EnemyState.Dying:
                Debug.Log("Dead");
                break;
            default:
                break;
        }
    }
}
