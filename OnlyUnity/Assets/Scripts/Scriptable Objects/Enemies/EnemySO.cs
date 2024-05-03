using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/New Enemy")]
public class EnemySO : ScriptableObject
{
    //self explainitory (spelling)
    public enum Type
    {
        Exploding,
        Ranged,
        Mele,
        Spawner
    }

    public enum Element
    {
        Normal,
        Ice,
        Fire,
        Earth,
        Air
    }

    [SerializeField] private LayerMask layerMask;



    public EnemyAttributes[] enemyArray;
  
    //Letar upp vilket värde som ska returneras beroende på vilken typ som angets
    public float GetMaxHP(Type typ)
    {
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (enemyArray[i].type == typ)
            {
                return enemyArray[i].health;
            }
        }
        return enemyArray[1].health;
    }
    public float GetBaseSpeed(Type typ)
    {
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (enemyArray[i].type == typ)
            {
                return enemyArray[i].speed;
            }
        }
        return enemyArray[1].speed;
    }
    public float GetBaseAttack(Type typ)
    {
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (enemyArray[i].type == typ)
            {
                return enemyArray[i].damage;
            }
        }
        return enemyArray[1].damage;
    }

    
    public void Attack(Type _type, Element _element, BaseEnemy caller)
    {
        switch (_type)
        {
            case Type.Exploding:
                ExlodingAttack(_element, caller);
                break;
            case Type.Ranged:
                RangedAttack(_element, caller);
                break;
            case Type.Mele:
                break;
            case Type.Spawner:
                break;
            default:
                break;
        }
    }

    private void ExlodingAttack(Element _element, BaseEnemy caller)
    {
        Collider[] damagableObjects;
        IDamagable temp;
        switch (_element)
        {
            case Element.Normal:
                damagableObjects = Physics.OverlapSphere(caller.transform.position, 10, layerMask);
                for (int i = 0; i < damagableObjects.Length; i++)
                {
                    if (damagableObjects[i].TryGetComponent<IDamagable>(out temp))
                    {
                        temp.TakeDamage(50);
                    }
                }
                //play animation or partical
                caller.TakeDamage(0);
                break;
            case Element.Ice:
                break;
            case Element.Fire:
                break;
            case Element.Earth:
                break;
            case Element.Air:
                break;
            default:
                break;
        }
    } 
    
    private void RangedAttack(Element _element, BaseEnemy caller)
    {

    }
}
[System.Serializable]
public struct EnemyAttributes
{
    public string name;
    public float health;
    public float speed;
    public float damage;
    public EnemySO.Type type;
}
