using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    /// <summary>
    /// Makes the Damagable object take damage
    /// </summary>
    /// <param name="damage"> The amount of damage the damagable object should take</param>
    /// <returns></returns>
    public void TakeDamage(float damage);
}
