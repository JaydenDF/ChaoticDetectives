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

        // Try to load an existing asset
        string path = $"Assets/Resources/Dialogues/{fileName}.asset";
        _dialogueContainer = AssetDatabase.LoadAssetAtPath<DialogueContainer>(path);

        // If the asset does not exist, create a new one
        if (_dialogueContainer == null)
        {
            _dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
            AssetDatabase.CreateAsset(_dialogueContainer, path);
        }
        else
        {
            // If it exists, clear its data to avoid duplicating entries
            _dialogueContainer.NodeLinks.Clear();
            _dialogueContainer.DialogueNodeData.Clear();
        }

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

        foreach (var dialogueNode in Nodes.Where(node => !node.SpecialNode.Equals(SpecialNodeType.Start)))
        {
            _dialogueContainer.DialogueNodeData.Add(new DialogueNodeData
            {
                NodeGUID = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                Position = dialogueNode.GetPosition().position
            });
        }

        // Check if there is a port named "Next" connected to something, if not show error
        if (_dialogueContainer.NodeLinks.Find(x => x.PortName == "Next") == null)
        {
            EditorUtility.DisplayDialog("Error", "The starting node must be connected to another node", "Agh, my bad :C I did an oopsi daisy! I'm sowwy!");
            return;
        }

        Debug.Log("Saving graph to: " + path);

        // Ensure the Resources and Dialogues folders exist
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Dialogues"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Dialogues");
        }

        // Save the asset
        EditorUtility.SetDirty(_dialogueContainer);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void LoadGraph(string fileName)
    {
        // Load the dialogue container from the specified file
        _dialogueContainer = Resources.Load<DialogueContainer>($"Dialogues/{fileName}");

        if (_dialogueContainer == null)
        {
            EditorUtility.DisplayDialog("File Not Found", "Target dialogue graph file does not exist!", "Oh im sowwy :c the keybord is hard to type on!");
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
                var targetNodeGuid = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                var port = (Port)Nodes[i].outputContainer[j].Q<Port>(); // Ensure the port is correctly selected
                port.portName = connections[j].PortName; // Set the port name correctly

                LinkNodes(port, (Port)targetNode.inputContainer[0]);
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
        Nodes.Find(x => x.SpecialNode.Equals(SpecialNodeType.Start)).GUID = _dialogueContainer.NodeLinks[0].BaseNodeGuid;

        foreach (var node in Nodes)
        {
            if (node.SpecialNode.Equals(SpecialNodeType.Start)) continue;
            Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => _graphView.RemoveElement(edge));
            _graphView.RemoveElement(node);
        }
    }

    private void CreateNodes(DialogueContainer dialogueContainer)
    {
        foreach (var nodeData in dialogueContainer.DialogueNodeData)
        {
            if(nodeData.DialogueText == "ENDPOINT") 
            {
                CreateEndNode(nodeData);
                continue;
            }
            
            //check if the node is an event node by accesing its port name
            string portName = dialogueContainer.NodeLinks.Find(x => x.BaseNodeGuid == nodeData.NodeGUID)?.PortName;
            if (portName == "Event")
            {
                _graphView.GenerateEventNode(nodeData);
                continue;
            }

            var tempNode = _graphView.CreateDialogueNode(nodeData.DialogueText);
            tempNode.GUID = nodeData.NodeGUID;
            _graphView.AddElement(tempNode);

            var nodePorts = dialogueContainer.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.NodeGUID).ToList();
            foreach (var nodePort in nodePorts)
            {
                if (string.IsNullOrWhiteSpace(nodePort.TargetNodeGuid)) continue;
                _graphView.AddChoicePort(tempNode, nodePort.PortName);
            }
        }
    }

    private void CreateEndNode(DialogueNodeData nodeData)
    {
        _graphView.GenerateEndNode(nodeData.NodeGUID);
    }
}