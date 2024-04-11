using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogText;

    [SerializeField] float letterPerSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    public static DialogManager Instance { get; private set; }
    void Awake()
    {
        //if (Instance == null)
        //    Instance = this;
        //else
        //    Destroy(this.gameObject);
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;
    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();
        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.GetDialog[0]));
    }
    public void HandleUpdate()
    {
        if (Keyboard.current.zKey.isPressed && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialog.GetDialog.Count)
                StartCoroutine(TypeDialog(dialog.GetDialog[currentLine]));
            else
            {
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
            }
        }
    }
    private IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1 / letterPerSecond);
        }
        isTyping = false;
    }
}
