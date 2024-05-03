using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { FreeRoam, Dialog, Dead}
public class GameManager : MonoBehaviour
{
    private State currentState = State.FreeRoam;
    [SerializeField] private PlayerMovement Player;
    // Start is called before the first frame update
    //public static GameManager Instance { get; private set; }
    //public void Awake()
    //{
    //    if (Instance != null)
    //        Destroy(gameObject);
    //    else
    //        Instance = this;
    //}
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
            case State.Dead:
                //Player dead;
                break;
            default:
                break;
        }
    }
    private void ChangeToDialog() => currentState = State.Dialog;
    private void ChangeToFreeRoam() { if (currentState == State.Dialog) currentState = State.FreeRoam; }

  //  public void ChangeState(State newState) => currentState = newState;
}
