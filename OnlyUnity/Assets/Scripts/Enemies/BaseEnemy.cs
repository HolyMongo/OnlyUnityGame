using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private EnemySO.Type type;
    [SerializeField] private EnemySO.Element element;
    [SerializeField] private float health;
    [SerializeField] private EnemyStates states;


    private void Start()
    {
        health = enemySO.GetMaxHP(type);
        states = transform.GetComponent<EnemyStates>();
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0)
        {
            states.CheatAnim(EnemyState.Dying);
        }
    }

    public void Attack()
    {
        enemySO.Attack(type, element, this);
    }
}
