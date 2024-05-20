using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarterInteractable : MonoBehaviour, IInteractable
{
    public static Action<DialogueContainer> OnDialogueStart;
    [SerializeField] private DialogueContainer _dialogueContainer;
    private const float ScaleMultiplier = 1.1f;
    private Vector2 _originalScale;

[ContextMenu("Click")]
    public void OnClick()
    {
        OnDialogueStart?.Invoke(_dialogueContainer);
    }

    public void OnHoverEnter()
    {
        _originalScale = transform.localScale;
        transform.localScale *= ScaleMultiplier;
    }

    public void OnHoverExit()
    {
        transform.localScale = _originalScale;
    }
}
