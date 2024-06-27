using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueResetter))]
public class DialogueGiver : MonoBehaviour
{
    public string firstChoice;
    private void Awake()
    {
        if (!_hasBeenCalled)
        {
            _hasBeenCalled = true;
            firstChoice = GetCurrentDialogueString();
        }
    }
    private bool _hasBeenCalled = false;
    [SerializeField] private List<DialoguePossibility> dialogueChoices = new List<DialoguePossibility>();
    internal DialogueContainer RequestDialogue()
    {


        foreach (DialoguePossibility dialogueChoice in dialogueChoices)
        {
            if (dialogueChoice.thisIsTheCorrectChoice)
            {
                return dialogueChoice.dialogueContainer;
            }
        }
        return null;
    }

    public void SetDialogueContainer(string choiceText)
    {
        if (_hasBeenCalled == false)
        {
            _hasBeenCalled = true;
            firstChoice = GetCurrentDialogueString();
        }

        foreach (DialoguePossibility dialogueChoice in dialogueChoices)
        {
            if (dialogueChoice.choiceCondition == choiceText)
            {
                dialogueChoice.thisIsTheCorrectChoice = true;
            }
            else
            {
                dialogueChoice.thisIsTheCorrectChoice = false;
            }
        }
    }

    public string GetCurrentDialogueString()
    {
        foreach (DialoguePossibility dialogueChoice in dialogueChoices)
        {
            if (dialogueChoice.thisIsTheCorrectChoice)
            {
                string choice = dialogueChoice.choiceCondition;
                return choice;
            }
        }
        return null;
    }
}
