using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] private List<string> _dialog;
    public List<string> GetDialog
    {
        get => _dialog;
    }
}
