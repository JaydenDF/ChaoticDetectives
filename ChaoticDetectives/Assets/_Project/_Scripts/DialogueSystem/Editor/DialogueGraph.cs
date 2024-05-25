using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using UnityEditor.UIElements;
using UnityEngine.UI;

using Button = UnityEngine.UIElements.Button;
using UnityEditor.PackageManager.Requests;
using System.Runtime.InteropServices;

#if UNITY_EDITOR

namespace DialogueSystem
{
    public class DialogueGraph : EditorWindow
    {
        private DialogueGraphView _graphView;
        private string fileName = "New Dialogue";
        TextField fileNameTextField;

        [MenuItem("ChaoticDetectives/DialogueGraph")]
        private static void ShowWindow()
        {
            var window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent("DialogueGraph");
        }

        public void ShowWindow(string name)
        {
            var window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent("DialogueGraph");
            window.LoadFromName(name);
        }

        private void OnEnable()
        {
            ConstructGraph();
            GenerateToolbar();
            GenerateMiniMap();
        }


        private void OnDisable()
        {
            if (_graphView != null) { rootVisualElement.Remove(_graphView); }
        }
        private void GenerateToolbar()
        {
            Toolbar toolbar = new Toolbar();

            fileNameTextField = new TextField("File Name:");
            fileNameTextField.SetValueWithoutNotify(fileName);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(evt => { fileName = evt.newValue; });

            toolbar.Add(fileNameTextField);

            Button saveButton = new Button(() => { RequestOperation(true); });
            saveButton.text = "Save Data";
            toolbar.Add(saveButton);

            Button loadButton = new Button(() => { RequestOperation(false); });
            loadButton.text = "Load Data";
            toolbar.Add(loadButton);

            var NodeCreateButton = new Button(() => { _graphView.CreateNode("DialogueNode"); });
            NodeCreateButton.text = "Create Node";
            toolbar.Add(NodeCreateButton);

            Button CreateEndNodeButton = new Button(() => { _graphView.GenerateEndNode(); });
            CreateEndNodeButton.text = "Create End Node";
            toolbar.Add(CreateEndNodeButton);

            Button CreateEventNodeButton = new Button(() => { _graphView.GenerateEventNode(); });
            CreateEventNodeButton.text = "Create Event Node";
            toolbar.Add(CreateEventNodeButton);

            rootVisualElement.Add(toolbar);
        }


        private void RequestOperation(bool save, string file = null)
        {
            var saveUtility = GraphSaveUtility.GetInstance(_graphView);
            if (save)
            {
                saveUtility.SaveGraph(file == null ? fileName : file);
            }
            else
            {
                saveUtility.LoadGraph(file == null ? fileName : file);
            }
        }

        private void LoadFromName(string name)
        {
            RequestOperation(false, name);
            fileNameTextField.SetValueWithoutNotify(name);
        }


        private void ConstructGraph()
        {
            _graphView = new DialogueGraphView();
            {
                name = "Dialogue Graph";
            }

            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void GenerateMiniMap()
        {
            var miniMap = new MiniMap { anchored = true };
            miniMap.SetPosition(new Rect(10, 30, 200, 140));
            _graphView.Add(miniMap);
        }
    }
}

#endif