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
        Ice,
        Fire,
        Earth,
        Air
    }





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

    
    public void Attack(Type _type, Element _element)
    {
        switch (_type)
        {
            case Type.Exploding:
                ExlodingAttack(_element);
                break;
            case Type.Ranged:
                RangedAttack(_element);
                break;
            case Type.Mele:
                break;
            case Type.Spawner:
                break;
            default:
                break;
        }
    }

    private void ExlodingAttack(Element _element)
    {
       
    } 
    
    private void RangedAttack(Element _element)
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
