using UnityEngine;

public class TestDialogueEventListener : MonoBehaviour {
    [SerializeField] private DialogueContainer _dialogueContainer;
    
    private void Awake() {
        _dialogueContainer.OnDialogueEnd += () => Debug.Log("Dialogue Ended");
        _dialogueContainer.OnDialogueEvent += (args) => Debug.Log($"Dialogue Event: {args.EventName}");
    }

}