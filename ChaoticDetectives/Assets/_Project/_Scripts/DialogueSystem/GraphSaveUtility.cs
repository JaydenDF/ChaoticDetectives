using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphSaveUtility
{
    private DialogueGraphView _graphView;

    private List<Edge> Edges => _graphView.edges.ToList();
    private List<DialogueNode> Nodes => _graphView.nodes.ToList().Cast<DialogueNode>().ToList();
    private DialogueContainer _dialogueContainer;

    public static GraphSaveUtility GetInstance(DialogueGraphView graphView)
    {
        return new GraphSaveUtility
        {
            _graphView = graphView
        };
    }

    public void SaveGraph(string fileName)
    {
        if (Edges.Count == 0) return;

        _dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (int i = 0; i < connectedPorts.Length; i++)
        {
            DialogueNode outputNode = connectedPorts[i].output.node as DialogueNode;
            DialogueNode inputNode = connectedPorts[i].input.node as DialogueNode;

            _dialogueContainer.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }

        foreach (var dialogueNode in Nodes.Where(node => !node.EntyPoint))
        {
            _dialogueContainer.DialogueNodeData.Add(new DialogueNodeData
            {
                NodeGUID = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                Position = dialogueNode.GetPosition().position
            });
        }

        Debug.Log("Saving graph to: " + fileName);

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }

        AssetDatabase.CreateAsset(_dialogueContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }
    public void LoadGraph(string fileName)
    {
        _dialogueContainer = Resources.Load<DialogueContainer>(fileName);

        if (_dialogueContainer == null)
        {
            EditorUtility.DisplayDialog("File Not Found", "Target dialogue graph file does not exist!", "OK");
            return;
        }

        ClearGraph();

        CreateNodes(_dialogueContainer);
        ConnectNodes(_dialogueContainer);
    }

    private void ConnectNodes(DialogueContainer dialogueContainer)
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            var connections = dialogueContainer.NodeLinks.Where(x => x.BaseNodeGuid == Nodes[i].GUID).ToList();
            for (int j = 0; j < connections.Count; j++)
            {
                Debug.Log(j);
                var targetNodeGuid = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                LinkNodes(Nodes[i].outputContainer.Q<Port>(), (Port)targetNode.inputContainer[j]);
                targetNode.SetPosition(new Rect(dialogueContainer.DialogueNodeData.First(x => x.NodeGUID == targetNodeGuid).Position, _graphView.DefaultNodeSize));
            }
        }
    }

    private void LinkNodes(Port port1, Port port2)
    {
        var tempEdge = new Edge
        {
            output = port1,
            input = port2
        };

        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);
        _graphView.Add(tempEdge);
    }

    private void ClearGraph()
    {
        Nodes.Find(x => x.EntyPoint).GUID = _dialogueContainer.NodeLinks[0].BaseNodeGuid;

        foreach (var node in Nodes)
        {
            if (node.EntyPoint) continue;
            Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => _graphView.RemoveElement(edge));
            _graphView.RemoveElement(node);
        }
    }

    private void CreateNodes(DialogueContainer dialogueContainer)
    {
        foreach (var nodeData in dialogueContainer.DialogueNodeData)
        {
            var tempNode = _graphView.CreateDialogueNode(nodeData.DialogueText);
            tempNode.GUID = nodeData.NodeGUID;
            _graphView.AddElement(tempNode);

            var nodePorts = dialogueContainer.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.NodeGUID).ToList();
            nodePorts.ForEach(x => _graphView.AddChoicePort(tempNode, x.PortName));
        }
    }
}