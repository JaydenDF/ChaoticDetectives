public class DialogueStep
{
    public string DialogueText;
    public string[] Responses;
    
    public DialogueStep(string dialogueText, string[] responses, DialogueEventArgs eventArgs = null)
    {
        DialogueText = dialogueText;
        Responses = responses;
    }
}