using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using UnityEditor.UIElements;
using UnityEngine.UI;

using Button = UnityEngine.UIElements.Button;

#if UNITY_EDITOR

namespace DialogueSystem
{
    public class DialogueGraph : EditorWindow
    {
        private DialogueGraphView _graphView;
        private string fileName = "New Dialogue";

        [MenuItem("ChaoticDetectives/DialogueGraph")]
        private static void ShowWindow()
        {
            var window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent("DialogueGraph");
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

            TextField fileNameTextField = new TextField("File Name:");
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

            rootVisualElement.Add(toolbar);
        }


        private void RequestOperation(bool save)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                EditorUtility.DisplayDialog("Invalid file name!", "Please enter a valid file name.", "OK");
                return;
            }

            var saveUtility = GraphSaveUtility.GetInstance(_graphView);
            if (save)
            {
                saveUtility.SaveGraph(fileName);
            }
            else
            {
                saveUtility.LoadGraph(fileName);
            }
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