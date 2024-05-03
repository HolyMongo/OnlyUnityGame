using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float reach;
    [SerializeField] private Vector3 size;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private Collider[] damagables;
    [SerializeField] private bool cooldownReady;


    private void Start()
    {
        PlayerInputActionScript inputActions = new PlayerInputActionScript();

        inputActions.Player.Fire.Enable();
        inputActions.Player.Fire.performed += Attack;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        cooldownReady = true;
    }
    public void Attack(InputAction.CallbackContext obj)
    {
        if (cooldownReady)
        {
            cooldownReady = false;
            StartCoroutine(Cooldown());
            damagables = null;
            damagables = Physics.OverlapBox(transform.position + reach/2 * transform.forward, size, Quaternion.identity, hitLayer);
            if (damagables.Length > 0)
            {
                Debug.Log("Found Thing To damage");
                foreach (Collider item in damagables)
                {
                    if (item.TryGetComponent<IDamagable>(out IDamagable damagable))
                    {
                        damagable.TakeDamage(attackDamage);
                    }
            
                }
            }
            else
            {
                Debug.Log("No damagable objects found");
            }
        }
        else
        {
            Debug.Log("Cooldown is not up yet");
        }
    }
}
