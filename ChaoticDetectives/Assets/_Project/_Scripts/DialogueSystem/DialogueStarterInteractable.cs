using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarterInteractable : MonoBehaviour, IInteractable
{
    public static Action<DialogueContainer> OnDialogueStart;

    private DialogueGiver _dialogueGiver;
    private DialogueContainer _dialogueContainer;
    private Vector2 _originalScale;

    private void Awake()
    {
        _dialogueGiver = GetComponent<DialogueGiver>();
    }

    public void OnClick()
    {
        _dialogueContainer = _dialogueGiver.RequestDialogue();
        OnDialogueStart?.Invoke(_dialogueContainer);
    }
    private void Start()
    {
        _originalScale = transform.localScale;
    }

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }

    public void GiveDialogueContainer(DialogueContainer dialogueContainer)
    {
        _dialogueContainer = dialogueContainer;
    }
}
