using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class SelectAndDisableChildrenEditor : EditorWindow
{
    private static bool isEnabled = false;
    private bool showShortcuts = false;
    private List<GameObject> gameObjects = new List<GameObject>();

    private const string PREFS_KEY = "SelectAndDisableChildrenGameObjects";

    [MenuItem("Tools/Select And Disable Tools")]
    public static void ShowWindow()
    {
        GetWindow<SelectAndDisableChildrenEditor>("Select And Disable Tools");
    }


    [MenuItem("Tools/Select And Disable Tools/Toggle Functionality #e")] // Shortcut: Shift + E
    public static void ToggleFunctionality()
    {
        isEnabled = !isEnabled;
        EditorWindow.GetWindow<SelectAndDisableChildrenEditor>().Repaint();
    }

    [MenuItem("Tools/Select And Disable Tools/Enable All #a")] // Shortcut: Shift + A
    public static void EnableAll()
    {
        EditorWindow.GetWindow<SelectAndDisableChildrenEditor>().SetActiveStateForAll(true);
    }

    [MenuItem("Tools/Select And Disable Tools/Disable All #s")] // Shortcut: Shift + S
    public static void DisableAll()
    {
        EditorWindow.GetWindow<SelectAndDisableChildrenEditor>().SetActiveStateForAll(false);
    }
    private void OnEnable() {
        Selection.selectionChanged += OnSelectionChanged;
    }

    private void OnDisable() {
        Selection.selectionChanged -= OnSelectionChanged;
    }

    private void OnGUI()
    {
        // Shortcuts foldout section
        showShortcuts = EditorGUILayout.Foldout(showShortcuts, "Shortcuts");
        if (showShortcuts)
        {
            GUI.color = Color.yellow;
            GUILayout.BeginVertical("box");
            GUILayout.Label("Toggle Functionality: Shift + E", EditorStyles.label);
            GUILayout.Label("Enable All: Shift + A", EditorStyles.label);
            GUILayout.Label("Disable All: Shift + S", EditorStyles.label);
            GUILayout.EndVertical();
            GUI.color = Color.white;
        }

        GUILayout.Label("Toggle Select And Disable Tools", EditorStyles.boldLabel);
        isEnabled = EditorGUILayout.Toggle("Enabled", isEnabled);

        EditorGUILayout.Space();

        GUILayout.Label("GameObjects to Toggle", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Selected GameObjects"))
        {
            AddSelectedGameObjects();
            SaveGameObjects();
        }

        if (GUILayout.Button("Clear All"))
        {
            gameObjects.Clear();
            SaveGameObjects();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        GUI.color = Color.green; // Set the color to green for the "Enable All" button
        if (GUILayout.Button("Enable All"))
        {
            SetActiveStateForAll(true);
        }
        GUI.color = Color.white; // Reset to default color

        GUI.color = Color.red; // Set the color to red for the "Disable All" button
        if (GUILayout.Button("Disable All"))
        {
            SetActiveStateForAll(false);
        }
        GUI.color = Color.white; // Reset to default color

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        for (int i = 0; i < gameObjects.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            gameObjects[i] = (GameObject)EditorGUILayout.ObjectField(gameObjects[i], typeof(GameObject), true);
            if (GUILayout.Button("Remove"))
            {
                gameObjects.RemoveAt(i);
                SaveGameObjects();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
    private void OnSelectionChanged()
    {
        if (!isEnabled) return;

        // Check if the selected object is a GameObject
        if (Selection.activeGameObject != null)
        {
            GameObject selectedGameObject = Selection.activeGameObject;

            // Check if the selected GameObject has a parent
            if (selectedGameObject.transform.parent != null)
            {
                // Get all child GameObjects
                foreach (Transform child in selectedGameObject.transform.parent)
                {
                    if (child.gameObject != selectedGameObject)
                    {
                        child.gameObject.SetActive(false);
                    }
                }

                selectedGameObject.SetActive(true);
            }
        }
    }

    private void AddSelectedGameObjects()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            if (!gameObjects.Contains(go))
            {
                gameObjects.Add(go);
            }
        }
    }

    private void SetActiveStateForAll(bool state)
    {
        foreach (GameObject go in gameObjects)
        {
            if (go != null)
            {
                go.SetActive(state);
            }
        }
    }

    private void SaveGameObjects()
    {
        List<int> gameObjectIDs = new List<int>();
        foreach (GameObject go in gameObjects)
        {
            if (go != null)
            {
                gameObjectIDs.Add(go.GetInstanceID());
            }
        }
        EditorPrefs.SetString(PREFS_KEY, string.Join(",", gameObjectIDs));
    }

    private void LoadGameObjects()
    {
        gameObjects.Clear();
        string savedIDs = EditorPrefs.GetString(PREFS_KEY, "");
        if (!string.IsNullOrEmpty(savedIDs))
        {
            string[] ids = savedIDs.Split(',');
            foreach (string id in ids)
            {
                int instanceID;
                if (int.TryParse(id, out instanceID))
                {
                    GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
                    if (go != null)
                    {
                        gameObjects.Add(go);
                    }
                }
            }
        }
    }
}
