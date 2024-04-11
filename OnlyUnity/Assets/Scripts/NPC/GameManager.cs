using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { FreeRoam, Dialog}
public class GameManager : MonoBehaviour
{
    private State currentState = State.FreeRoam;
    [SerializeField] private PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        DialogManager.Instance.OnShowDialog += ChangeToDialog;
        DialogManager.Instance.OnHideDialog += ChangeToFreeRoam;
    }


    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.FreeRoam:
                Player.HandleUpdate();
                break;
            case State.Dialog:
                DialogManager.Instance.HandleUpdate();
                break;
            default:
                break;
        }
    }
    private void ChangeToDialog() => currentState = State.Dialog;
    private void ChangeToFreeRoam() { if (currentState == State.Dialog) currentState = State.FreeRoam; }
}
