using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Npc, dialogue, their character model
/// </summary>
public class NPC : Interactable
{
    [SerializeField] protected Image _ImageHolder;
    [SerializeField] protected NPCData _data;

    protected bool _dialogueStarted = false;
    protected int _dialogueIndex = 0;

    public override void InteractAction(OrbThrownData data) {
        base.InteractAction(data);
        if (!_dialogueStarted) { DialogueStart(); }
    }

    protected void DialogueStart() {
        _dialogueStarted = true;
        _ImageHolder.gameObject.SetActive(true);
        GameManager.Instance.EnterUIControls();
        GameManager.Instance.PlayerActionMap.UIControl.Continue.performed += ContinueDialogue;
    }

    protected void ContinueDialogue(InputAction.CallbackContext context) {
        if (_dialogueIndex == _data.ImageSeq.Count) {  EndDialoge(); }


    }

    protected void EndDialoge() {

    }
}
