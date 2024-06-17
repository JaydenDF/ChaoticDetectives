using System;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnDialogueEvent : MonoBehaviour {
    [SerializeField] private DialogueContainer dialogueContainer;
    [SerializeField] private string eventName;
    public UnityEvent OnEventTriggered;
    
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
            OnEventTriggered.Invoke();
        }
    }
}