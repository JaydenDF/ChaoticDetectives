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

        // Find the dialogue text for the target node GUID
        string dialogueText = DialogueNodeData.Find(x => x.NodeGUID == firstNodeGuid).DialogueText;

        // Find the responses for the target node GUID
        List<string> responses = new List<string>();
        foreach (NodeLinkData nodeLink in NodeLinks)
        {
            if (nodeLink.BaseNodeGuid == firstNodeGuid)
            {
                responses.Add(DialogueNodeData.Find(x => x.NodeGUID == nodeLink.TargetNodeGuid).DialogueText);
            }
        }


        

        return new DialogueStep(dialogueText, responses.ToArray());
    }

}