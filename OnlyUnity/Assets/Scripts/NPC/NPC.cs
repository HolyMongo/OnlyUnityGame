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
        if (other.gameObject.CompareTag("Player"))
        {
            canInteractDialog.SetActive(true);
            Debug.Log("Player entered");
        }
          
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canInteractDialog.SetActive(false);
    }
}
