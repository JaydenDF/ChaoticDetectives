using System;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnDialogueEvent : MonoBehaviour, IReset
 {
    [SerializeField] private DialogueContainer dialogueContainer;
    [SerializeField] private string eventName;
    public UnityEvent OnEventTriggered;

    private bool _hasTriggered = false;
    
    private void OnEnable() {
        dialogueContainer.OnDialogueEvent += OnDialogueEvent;
    }

    private void OnDisable() {
        dialogueContainer.OnDialogueEvent -= OnDialogueEvent;
    }

    private void OnDialogueEvent(DialogueEventArgs args)
    {
        if (args.EventName == eventName)
        {
            if (_hasTriggered) return;
            OnEventTriggered.Invoke();
            _hasTriggered = true;
        }
    }

    public void Reset()
    {
        _hasTriggered = false;
    }
}