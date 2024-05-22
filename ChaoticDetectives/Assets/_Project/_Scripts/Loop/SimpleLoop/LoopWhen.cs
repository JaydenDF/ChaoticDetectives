using System;
using UnityEngine;

public class LoopWhen : MonoBehaviour {
    
    private SimpleLoop _simpleLoop;
    private bool _dialogueFinished = false;

    private void OnEnable() {
        DialogueManager.OnDialogueEnd += OnDialogueEnd;
    }

    private void OnDisable() {
        DialogueManager.OnDialogueEnd -= OnDialogueEnd;
    }

    private void Awake() {
        _simpleLoop = GetComponent<SimpleLoop>();
    }

    private void OnDialogueEnd()
    {
        _dialogueFinished = true;
        CheckConditions();
    }

    private void CheckConditions()
    {
        if (_dialogueFinished)
        {
            _simpleLoop.Loop();
        }
    }
}