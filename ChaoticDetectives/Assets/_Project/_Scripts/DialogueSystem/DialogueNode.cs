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
        public bool EntyPoint = false;
        public bool ExitPoint = false;
        public DialogueNode()
        {
            title = "Dialogue Node";
        }
    }
}

#endif