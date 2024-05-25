using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;


#if UNITY_EDITOR
namespace DialogueSystem
{
    public class DialogueNode : Node
    {

        public string DialogueText;
        public string GUID;
        public SpecialNodeType SpecialNode = SpecialNodeType.None;
        public DialogueNode()
        {
            title = "Dialogue Node";
        }

    }

    
        public enum SpecialNodeType
        {
            Start,
            End,
            None,
            Event
        }
}

#endif