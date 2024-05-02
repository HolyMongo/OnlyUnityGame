using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    public int Health => _health;
    public PlayAudio DamageSound;

    public event Action OnRecievedDamage;
    public event Action OnDead;
    public event Action OnretrievedHealth;

    private bool isAlive = true;
    public static PlayerHealth Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        OnRecievedDamage += DamageSound.PlaySound;
    }
    public void RetrievedHealth(int amount)
    {
        OnretrievedHealth?.Invoke();
    }
    public void RecievedDamage(int amount)
    {
        if(isAlive)
        {
            _health -= amount;
            OnRecievedDamage?.Invoke();

            if (_health <= 0) isAlive = false;
        }
        else if(!isAlive)
        {
            Died();
        }
       
    }
    private void Died()
    {
        //Player is dead
        OnDead?.Invoke();
    }
}
