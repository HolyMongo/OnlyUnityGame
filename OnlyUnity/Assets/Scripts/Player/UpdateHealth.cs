using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateHealth : MonoBehaviour
{
    public TMP_Text health;

    public void Start()
    {
        PlayerHealth.Instance.OnRecievedDamage += UpdateUI;
        health.text = "Health " + PlayerHealth.Instance.Health;
    }

    public void UpdateUI()
    {
        health.text = "Health " + PlayerHealth.Instance.Health;
    }

}
