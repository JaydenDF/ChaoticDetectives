using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class DialogueContainer : ScriptableObject
{

    public Action OnDialogueEnd;
    public Action<DialogueEventArgs> OnDialogueEvent;

    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();


    /// <summary>
    /// Get the starting dialogue step
    /// </summary>

    public DialogueStep GetStartingStep()
    {
        // Find the target node GUID connected to the port "Next"
        string firstNodeGuid = NodeLinks.Find(x => x.PortName == "Next").TargetNodeGuid;

        string dialogueText = GetDialogueText(firstNodeGuid);
        string[] responses = GetResponses(firstNodeGuid);

        return new DialogueStep(dialogueText, responses);
    }

    /// <summary>
    /// Get the next dialogue step based on the index of the response given
    /// </summary>
    /// <param name="previousDialogue"></param>
    /// <param name="index"></param>
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

        if(IsNextNodeEndNode(nextNodeGuid))
        {
            OnDialogueEnd?.Invoke();
            return null;
        }

        string dialogueText = GetDialogueText(nextNodeGuid);
        string[] responses = GetResponses(nextNodeGuid);


                
        if(NodeLinks.Find(x => x.TargetNodeGuid == nextNodeGuid && x.PortName == "Event") != null)
        {
            string eventGuid = NodeLinks.Find(x => x.TargetNodeGuid == nextNodeGuid && x.PortName == "Event").BaseNodeGuid;
            string eventName = DialogueNodeData.Find(x => x.NodeGUID == eventGuid).DialogueText;
            DialogueEventArgs eventArgs = new DialogueEventArgs(eventName);
            OnDialogueEvent?.Invoke(eventArgs);
        }

        DialogueStep nextDialogue = new DialogueStep(dialogueText, responses);
        return nextDialogue;
    }

    /// <summary>
    /// Get the next dialogue step based on the response given
    /// </summary>
    /// <param name="previousDialogue"></param>
    /// <param name="response"></param>
    /// <returns></returns>
    public DialogueStep GetNextStepFromResponse(DialogueStep previousDialogue, string response)
    {
        //get guid with the previous dialogue text
        string previousNodeGuid = DialogueNodeData.Find(x => x.DialogueText == previousDialogue.DialogueText).NodeGUID;

        string nextNodeGuid = NodeLinks.Find(x => x.BaseNodeGuid == previousNodeGuid && DialogueNodeData.Find(y => y.NodeGUID == x.TargetNodeGuid).DialogueText == response).TargetNodeGuid;

        if(IsNextNodeEndNode(nextNodeGuid))
        {
            OnDialogueEnd?.Invoke();
            return null;
        }

        string dialogueText = GetDialogueText(nextNodeGuid);
        string[] responses = GetResponses(nextNodeGuid);

        DialogueStep nextDialogue = new DialogueStep(dialogueText, responses);
        return nextDialogue;
    }

    private string[] GetResponses(string nodeGuid)
    {
        //get all the ports under the node guid in linkdata
        List<NodeLinkData> nodeLinks = NodeLinks.Where(x => x.BaseNodeGuid == nodeGuid).ToList();
        return nodeLinks.Select(x => x.PortName).ToArray();
    }

    private string GetDialogueText(string nodeGuid)
    {
        return DialogueNodeData.Find(x => x.NodeGUID == nodeGuid).DialogueText;
    }

    private bool IsNextNodeEndNode(string nodeGuid)
    {
        if (DialogueNodeData.Find(x => x.NodeGUID == nodeGuid).DialogueText == "ENDPOINT")
        {
            return true;
        }
        return false;
    }
}

