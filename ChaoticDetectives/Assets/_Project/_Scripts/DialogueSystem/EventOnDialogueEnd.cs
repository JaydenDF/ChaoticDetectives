using UnityEngine;

public class EventOnDialogueEnd : MonoBehaviour {
    public DialogueContainer container;
    public UnityEngine.Events.UnityEvent OnDialogueEnd;

    private void OnEnable() {
        container.OnDialogueEnd += OnDialogueEnd.Invoke;
    }

    private void OnDisable() {
        container.OnDialogueEnd -= OnDialogueEnd.Invoke;
    }
}