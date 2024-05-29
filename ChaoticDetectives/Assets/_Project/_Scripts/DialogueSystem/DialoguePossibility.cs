using UnityEngine;

[System.Serializable]
public class DialoguePossibility
{
    public string choiceCondition;
    public bool thisIsTheCorrectChoice = false;
    public DialogueContainer dialogueContainer;
}