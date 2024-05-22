using System;
using System.Collections;
using UnityEngine;

public class LoopWhen : MonoBehaviour {
    
    public static Action OnLooped;
    private SimpleLoop _simpleLoop;
    private bool _dialogueFinished = false;
    private bool _plantPlanted = false;

    private void OnEnable() {
        PlantInteractable.PlantPlanted += OnPlantPlanted;
        DialogueManager.OnDialogueEnd += OnDialogueEnd;
    }

    private void OnDisable() {
        PlantInteractable.PlantPlanted -= OnPlantPlanted;
        DialogueManager.OnDialogueEnd -= OnDialogueEnd;
    }

    private void OnPlantPlanted()
    {
        _plantPlanted = true;
        CheckConditions();
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
        if (_dialogueFinished && _plantPlanted)
        {
            OnLooped?.Invoke();
            _simpleLoop.Loop();
        }
    }
}