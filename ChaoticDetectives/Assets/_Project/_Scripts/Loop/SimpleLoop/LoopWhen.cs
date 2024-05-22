using System;
using UnityEngine;

public class LoopWhen : MonoBehaviour {
    
    private SimpleLoop _simpleLoop;
    private bool _dialogueFinished = false;
    private bool _plantPlanted = false;

    private void OnEnable() {
        PlantInteractable.PlantPlanted += () => _plantPlanted = true;
        DialogueManager.OnDialogueEnd += OnDialogueEnd;
    }

    private void OnDisable() {
        PlantInteractable.PlantPlanted -= () => _plantPlanted = true;
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