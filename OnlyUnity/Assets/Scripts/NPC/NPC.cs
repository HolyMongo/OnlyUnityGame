using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog dialog;
    [SerializeField] private GameObject canInteractObject; // Floating object
    [SerializeField] private GameObject canInteractCrosshair; // Interact color
    [SerializeField] private GameObject InteractImage; // Image of NPC
    [SerializeField] private bool canInteract = true;
    public GameObject eye;
    [SerializeField] private Color highlightColor; // Color to change to when player enters trigger

    private Image crosshairImage;
    private Color originalColor;


    // !!!!!!!!!!!!!!!!!!!!!! SHOULD IMPLEMENT CROSSAIR SOMEWHERE ELSE, NOT ONLY FOR NPC. SO SHOULD MOVE THAT CODE


    private void Start()
    {
        DialogManager.Instance.OnHideDialog += HideDialog;
        // Get the Image component of the crosshair GameObject
        crosshairImage = canInteractCrosshair.GetComponent<Image>();
        // Store the original color of the crosshair
        originalColor = crosshairImage.color;
    }

    private void HideDialog()
    {
        InteractImage.SetActive(false);
    }

    public void Interact()
    {
        if(canInteract)
        {
            InteractImage.SetActive(true);
           StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
            canInteract = false;
            canInteractObject.SetActive(false);
            crosshairImage.color = originalColor;
   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canInteract)
        {
            crosshairImage.color = highlightColor;
            Debug.Log("Player entered");
        }
          
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canInteract)
        {
            Vector3 direction = other.gameObject.transform.position - eye.transform.position;

            // Ignore rotation in the x and z axes
            direction.y = 0f;

            // Rotate the eye to look in the new direction
            eye.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canInteract)
            crosshairImage.color = originalColor;
    }
}
