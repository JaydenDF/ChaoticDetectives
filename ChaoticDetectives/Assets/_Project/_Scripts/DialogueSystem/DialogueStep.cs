public class DialogueStep
{
    public string DialogueText;
    public string[] Responses;
    
    public DialogueStep(string dialogueText, string[] responses)
    {
        DialogueText = dialogueText;
        Responses = responses;
    }
}