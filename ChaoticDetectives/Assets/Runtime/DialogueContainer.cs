using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DialogueContainer : ScriptableObject
{
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();

    public DialogueStep GetStartingStep()
    {
        // Find the target node GUID connected to the port "Next"
        string firstNodeGuid = NodeLinks.Find(x => x.PortName == "Next").TargetNodeGuid;

        string dialogueText = GetDialogueText(firstNodeGuid);
        string[] responses = GetResponses(firstNodeGuid);

        return new DialogueStep(dialogueText, responses);
    }

    public DialogueStep GetNextStepFromIndex(DialogueStep previousDialogue, int index)
    {
        //get guid with the previous dialogue text
        string previousNodeGuid = DialogueNodeData.Find(x => x.DialogueText == previousDialogue.DialogueText).NodeGUID;

        List<string> targetNodeGuids = new List<string>();

        foreach (NodeLinkData nodeLink in NodeLinks)
        {
            if (nodeLink.BaseNodeGuid == previousNodeGuid)
            {
                targetNodeGuids.Add(nodeLink.TargetNodeGuid);
            }
        }

        string nextNodeGuid = targetNodeGuids[index];

        string dialogueText = GetDialogueText(nextNodeGuid);
        string[] responses = GetResponses(nextNodeGuid);

        DialogueStep nextDialogue = new DialogueStep(dialogueText, new string[0]);
        return nextDialogue;
    }

    public DialogueStep GetNextStepFromResponse(DialogueStep previousDialogue, string response)
    {
        //get guid with the previous dialogue text
        string previousNodeGuid = DialogueNodeData.Find(x => x.DialogueText == previousDialogue.DialogueText).NodeGUID;

        string nextNodeGuid = NodeLinks.Find(x => x.BaseNodeGuid == previousNodeGuid && DialogueNodeData.Find(y => y.NodeGUID == x.TargetNodeGuid).DialogueText == response).TargetNodeGuid;

        string dialogueText = GetDialogueText(nextNodeGuid);
        string[] responses = GetResponses(nextNodeGuid);

        DialogueStep nextDialogue = new DialogueStep(dialogueText, responses);
        return nextDialogue;
    }

    private string[] GetResponses(string nodeGuid)
    {
        List<string> responses = new List<string>();
        foreach (NodeLinkData nodeLink in NodeLinks)
        {
            if (nodeLink.BaseNodeGuid == nodeGuid)
            {
                responses.Add(DialogueNodeData.Find(x => x.NodeGUID == nodeLink.TargetNodeGuid).DialogueText);
            }
        }
        return responses.ToArray();
    }

    private string GetDialogueText(string nodeGuid)
    {
        return DialogueNodeData.Find(x => x.NodeGUID == nodeGuid).DialogueText;
    }
}