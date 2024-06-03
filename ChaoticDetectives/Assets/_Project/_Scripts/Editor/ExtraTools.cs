using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SelectAndDisableChildrenEditor : EditorWindow
{
    private static bool isEnabled = false;
    private List<GameObject> gameObjects = new List<GameObject>();

    private const string PREFS_KEY = "SelectAndDisableChildrenGameObjects";

    [MenuItem("Window/Select And Disable Tools %#d")] // Shortcut: Ctrl/Cmd + Shift + D
    public static void ShowWindow()
    {
        GetWindow<SelectAndDisableChildrenEditor>("Select And Disable Tools");
    }

    [MenuItem("Window/Toggle Select And Disable Tools %#t")] // Shortcut: Ctrl/Cmd + Shift + T
    public static void ToggleFunctionality()
    {
        isEnabled = !isEnabled;
        EditorWindow.GetWindow<SelectAndDisableChildrenEditor>().Repaint();
    }

    private void OnGUI()
    {
        GUILayout.Label("Toggle Select And Disable Tools", EditorStyles.boldLabel);
        isEnabled = EditorGUILayout.Toggle("Enabled", isEnabled);

        EditorGUILayout.Space();

        GUILayout.Label("GameObjects to Toggle", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Selected GameObjects"))
        {
            AddSelectedGameObjects();
            SaveGameObjects();
        }

        EditorGUILayout.Space();

      

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

        if(GUILayout.Button("Clear All"))
        {
            gameObjects.Clear();
            SaveGameObjects();
        }
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;
        LoadGameObjects();
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= OnSelectionChanged;
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
