using UnityEngine;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private DialogueContainer dialogueContainer;
    private DialogueStep currentDialogueStep;

    [ContextMenu("Print First Dialogue Step")]
    public void PrintFirstDialogueStep()
    {
        currentDialogueStep = dialogueContainer.GetStartingStep();
        Debug.Log(currentDialogueStep.DialogueText);
        foreach (string response in currentDialogueStep.Responses)
        {
            Debug.Log(response);
        }

    }
}
