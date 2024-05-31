using UnityEngine;

public class ChanceEventOnDialogueEnd : ChanceEventStarter
{
    [SerializeField] private DialogueContainer _dialogueToReactTo;

    private void OnEnable() {
        _dialogueToReactTo.OnDialogueEnd += ChanceEvent;
    }
}