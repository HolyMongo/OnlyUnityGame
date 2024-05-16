using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

enum HealthState
{
    Strong,
    Weak,
    Dying
}
public class UpdateHealth : MonoBehaviour
{
    public TMP_Text health;
    public Slider healthSlider;
    public GameObject healthColor;
    public Sprite[] healthImages;
    public Image healthImage;
    private HealthState healthState;
    public Color StartColor;
    public GameObject DeathUIObject;

    //Disable PlayerMovement and CameraLookAround when dead
    [SerializeField] private PlayerController playerMovement;
    //[SerializeField] private PlayerMouseLook playerMouseLook;

    public void Start()
    {
        PlayerHealth.Instance.OnRecievedDamage += UpdateUI;
        PlayerHealth.Instance.OnDead += DeathUI;
        // health.text = "Health " + PlayerHealth.Instance.Health;
        healthSlider.value = PlayerHealth.Instance.Health;
    }

    public void UpdateUI()
    {
        //health.text = "Health " + PlayerHealth.Instance.Health;
        healthSlider.value = PlayerHealth.Instance.Health;
        CheckHealthState();
    }
    private void CheckHealthState()
    {
        float currentHealth = PlayerHealth.Instance.Health;
        if (currentHealth > 75)
            healthState = HealthState.Strong;
        else if ((currentHealth > 40))
            healthState = HealthState.Weak;
        else
            healthState = HealthState.Dying;

        switch (healthState)
        {
            case HealthState.Strong:
                healthImage.sprite = healthImages[0];
                //healthColor.GetComponent<Image>().color = Color.white;
                break;
            case HealthState.Weak:
                healthImage.sprite = healthImages[1];
                healthColor.GetComponent<Image>().color = Color.yellow;
                break;
            case HealthState.Dying:
                healthImage.sprite = healthImages[2];
                healthColor.GetComponent<Image>().color = Color.red;
                break;
            default:
                break;
        }
    }
    private void DeathUI()
    {
        playerMovement.enabled = false;
       // playerMouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        DeathUIObject.SetActive(true);
    }
}
