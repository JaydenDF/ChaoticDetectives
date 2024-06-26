using UnityEngine;

public class DialogueResetter : MonoBehaviour, IReset
{
    private DialogueGiver _dialogueGiver;
    private string _initialChoice;

    private void Start()
    {
        _dialogueGiver = GetComponent<DialogueGiver>();

        _initialChoice = _dialogueGiver.GetCurrentDialogueString();
    }
    public void Reset()
    {
        if (_dialogueGiver != null)
        {
            _dialogueGiver.SetDialogueContainer(_initialChoice);
        }
        else
        {
            _dialogueGiver = GetComponent<DialogueGiver>();
            _dialogueGiver.SetDialogueContainer(_initialChoice);
        }
    }
}