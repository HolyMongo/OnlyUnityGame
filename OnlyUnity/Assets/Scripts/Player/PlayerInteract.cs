using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask nonWalkable;
    public Collider[] target;
    [SerializeField] private GameObject canInteractDialog;
    void OnInteract()
    {
        canInteractDialog.SetActive(false);
        target = null;
        target = Physics.OverlapSphere(transform.position, 0.5f, nonWalkable);
        if (target.Length > 0)
        {
            if(target[0].gameObject.layer == LayerMask.NameToLayer("NPC"))
            {
                Debug.Log("Inside of NPC");
                target[0].GetComponent<IInteractable>().Interact();
            }
           
        }
    }
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.collider.isTrigger && hit.gameObject.layer == LayerMask.NameToLayer("NPC"))
    //    {
    //        Debug.Log("Show text");
    //      //  canInteractDialog.SetActive(true);
    //    }
    //}
}
