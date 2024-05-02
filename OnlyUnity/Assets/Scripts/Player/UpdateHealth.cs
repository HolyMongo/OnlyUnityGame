using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateHealth : MonoBehaviour
{
    public TMP_Text health;

    public void Start()
    {
        health.text = "Health " + PlayerHealth.Instance.Health;
    }

    public void UpdateUI()
    {
        health.text = "Health " + PlayerHealth.Instance.Health;
    }
    public void OnEnable()
    {
        PlayerHealth.Instance.OnRecievedDamage += UpdateUI;
    }
    public void OnDisable()
    {
        PlayerHealth.Instance.OnRecievedDamage -= UpdateUI;
    }
}
