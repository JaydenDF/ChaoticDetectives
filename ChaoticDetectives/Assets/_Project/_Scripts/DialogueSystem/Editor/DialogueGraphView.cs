using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using System.Numerics;

using Direction = UnityEditor.Experimental.GraphView.Direction;
using Vector2 = UnityEngine.Vector2;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

#if UNITY_EDITOR

namespace DialogueSystem
{
    public class DialogueGraphView : GraphView
    {
        public readonly Vector2 DefaultNodeSize = new(150, 300);

        public DialogueGraphView()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            GridBackground grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
            this.styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph"));

            AddElement(GenerateEntryPointNode());
        }

        private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }
        private DialogueNode GenerateEntryPointNode()
        {
            DialogueNode node = new DialogueNode
            {
                title = "Start",
                GUID = Guid.NewGuid().ToString(),
                DialogueText = "ENTRYPOINT",
                EntryPoint = true
            };

            node.styleSheets.Add(Resources.Load<StyleSheet>("EntryPointNode"));

            var generatedPort = GeneratePort(node, Direction.Output);
            generatedPort.portName = "Next";

            node.outputContainer.Add(generatedPort);
            node.capabilities &= ~Capabilities.Deletable;

            node.RefreshExpandedState();
            node.RefreshPorts();


            node.SetPosition(new Rect(new Vector2(200, 100), DefaultNodeSize));
            return node;
        }

        public void CreateNode(string nodeName)
        {
            AddElement(CreateDialogueNode(nodeName));
        }
        public DialogueNode CreateDialogueNode(string name)
        {
            DialogueNode node = new DialogueNode
            {
                title = name,
                GUID = Guid.NewGuid().ToString(),
                DialogueText = name,
            };

            node.styleSheets.Add(Resources.Load<StyleSheet>("DialogueNode"));

            var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Single);
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);

            Button button = new Button(() => { AddChoicePort(node); });
            button.text = "New Choice";
            node.titleContainer.Add(button);

            var textField = new TextField(string.Empty);
            textField.RegisterValueChangedCallback(evt =>
            {
                node.DialogueText = evt.newValue;
                node.title = evt.newValue;
            });

            textField.SetValueWithoutNotify(node.title);
            node.mainContainer.Add(textField);

            node.RefreshExpandedState();
            node.RefreshPorts();

            node.SetPosition(new Rect(new Vector2(200, 200), DefaultNodeSize));

            return node;
        }
        public void AddChoicePort(DialogueNode node, string overriddenPortName = "")
        {
            Port generatedPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);

            var labelToRemove = generatedPort.contentContainer.Q<Label>("type");
            generatedPort.contentContainer.Remove(labelToRemove);

            var choicePortName = string.IsNullOrEmpty(overriddenPortName) ? $"Choice {node.outputContainer.Query("connector").ToList().Count + 1}" : overriddenPortName;

            var textField = new TextField
            {
                name = string.Empty,
                value = choicePortName
            };

            textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);

            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            Button deleteButton = new Button(() => RemovePort(node, generatedPort))
            {
                text = "X",
            };
            container.Add(deleteButton); // Add the delete button
            container.Add(textField); // Add the text field
            container.Add(generatedPort); // Add the port

            node.outputContainer.Add(container);

            node.RefreshPorts();
            node.RefreshExpandedState();
        }




        private void RemovePort(DialogueNode node, Port generatedPort)
        {
            var targetEdge = edges.ToList().Where(x => x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);

            if (targetEdge.Any())
            {
                Edge edge = targetEdge.First();
                edge.input.Disconnect(edge);
                RemoveElement(targetEdge.First());

                // Remove the port from the visual tree which is in the output containers container
                //first find the container that contains the port
                VisualElement container = generatedPort.GetFirstAncestorOfType<VisualElement>();
                node.outputContainer.Remove(container);


                node.RefreshPorts();
                node.RefreshExpandedState();
            }
            else
            {
                VisualElement container = generatedPort.GetFirstAncestorOfType<VisualElement>();
                node.outputContainer.Remove(container);

                node.RefreshPorts();
                node.RefreshExpandedState();
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            ports.ForEach((port) =>
            {
                if (startPort != port && startPort.node != port.node)
                {
                    compatiblePorts.Add(port);
                }
            });

            return compatiblePorts;
        }
    }

}
#endif