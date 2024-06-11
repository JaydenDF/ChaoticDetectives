using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Linq;
using System;

public class SelectAndDisableChildrenEditor : EditorWindow
{
    private static bool isEnabled = false;
    private List<GameObject> gameObjects = new List<GameObject>();
    private const string ASSET_PATH = "Assets/Resources/SceneObjectMappings.asset";
    private SceneObjectMappings sceneObjectMappings;

    [MenuItem("Tools/Select And Disable Tools")]
    public static void ShowWindow()
    {
        GetWindow<SelectAndDisableChildrenEditor>("Select And Disable Tools");
    }
    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;
        EditorSceneManager.sceneOpened += OnSceneOpened;
        LoadMappings();
        LoadGameObjects();
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= OnSelectionChanged;
        EditorSceneManager.sceneOpened -= OnSceneOpened;
        SaveMappings();
    }

    private void OnSceneOpened(Scene scene, OpenSceneMode mode)
    {
        LoadGameObjects();
    }

    private void OnGUI()
    {

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

        if (GUI.changed)
        {
            SaveGameObjects();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Load Game Objects"))
        {
            LoadGameObjects();
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
        string sceneName = SceneManager.GetActiveScene().name;
        List<string> gameObjectNames = new List<string>();
        foreach (GameObject go in gameObjects)
        {
            if (go != null)
            {
                gameObjectNames.Add(go.name);
            }
        }
        if(sceneObjectMappings == null)
        {
            return;
        }
        SceneObjectMappings.SceneMapping mapping = sceneObjectMappings.sceneMappings.Find(m => m.sceneName == sceneName);
        if (mapping != null)
        {
            mapping.gameObjectNames = gameObjectNames;
        }
        else
        {
            sceneObjectMappings.sceneMappings.Add(new SceneObjectMappings.SceneMapping
            {
                sceneName = sceneName,
                gameObjectNames = gameObjectNames
            });
        }

        SaveMappings();
    }

    private void LoadGameObjects()
    {
        gameObjects.Clear();
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneObjectMappings == null)
        {
            return;
        }
        SceneObjectMappings.SceneMapping mapping = sceneObjectMappings.sceneMappings.Find(m => m.sceneName == sceneName);
        if (mapping != null)
        {
            foreach (string name in mapping.gameObjectNames)
            {
                GameObject go = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<Transform>(true)).Select(x => x.gameObject).FirstOrDefault(x => x.name == name);

                if (go != null)
                {
                    gameObjects.Add(go);
                }
            }
        }
    }

    private void SaveMappings()
    {
        if (sceneObjectMappings == null)
        {
            return;
        }
        EditorUtility.SetDirty(sceneObjectMappings);
        AssetDatabase.SaveAssets();
    }

    private void LoadMappings()
    {
        sceneObjectMappings = Resources.Load<SceneObjectMappings>("SceneObjectMappings");
    }
}

