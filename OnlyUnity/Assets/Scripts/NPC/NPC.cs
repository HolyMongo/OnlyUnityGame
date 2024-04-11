using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog dialog;
    [SerializeField] private GameObject canInteractObject;
    [SerializeField] private GameObject canInteractDialog;
    [SerializeField] private bool canInteract = true;
    public GameObject eye;
    // [SerializeField] private GameObject canInteractObject;
    public void Interact()
    {
        if(canInteract)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
            canInteract = false;
            canInteractObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canInteract)
        {
            canInteractDialog.SetActive(true);
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
            canInteractDialog.SetActive(false);
    }
}
