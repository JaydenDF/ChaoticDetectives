using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGiver : MonoBehaviour
{
    [SerializeField] private  List<DialoguePossibility> dialogueChoices = new List<DialoguePossibility>();
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
}
