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
    [SerializeField] protected NPCData _npcData;

    protected bool _dialogueStarted = false;
    protected int _dialogueIndex = 0;

    public override void InteractAction(OrbThrownData data) {
        base.InteractAction(data);
        if (!_dialogueStarted) { DialogueStart(); }
    }

    protected void DialogueStart() {
        _dialogueIndex = 0;
        _dialogueStarted = true;
        _ImageHolder.gameObject.SetActive(true);
        GameManager.Instance.EnterUIControls();
        GameManager.Instance.PlayerActionMap.UIControl.Continue.performed += ContinueDialogue;
        _ImageHolder.sprite = _npcData.ImageSeq[_dialogueIndex];
    }

    protected void ContinueDialogue(InputAction.CallbackContext context) {
        _dialogueIndex++;

        if (_dialogueIndex == _npcData.ImageSeq.Count) {  EndDialoge(); return;  }

        _ImageHolder.sprite = _npcData.ImageSeq[_dialogueIndex];
    }

    protected void EndDialoge() {
        _ImageHolder.gameObject.SetActive(false);
        GameManager.Instance.EnterPlayerControls();
    }
}
