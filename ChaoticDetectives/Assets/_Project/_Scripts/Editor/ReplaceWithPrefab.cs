using UnityEngine;
using UnityEditor;

public class ReplaceWithPrefab : EditorWindow
{
    private GameObject prefabToReplaceWith;

    [MenuItem("Tools/ReplaceWithPrefab")]
    private static void ShowWindow()
    {
        var window = GetWindow<ReplaceWithPrefab>();
        window.titleContent = new GUIContent("Replace With Prefab");
        window.Show();
    }

    private void OnGUI()
    {
        if (Selection.activeGameObject != null)
        {
            GUILayout.Label("Selected GameObject: " + Selection.activeGameObject.name);

            if (GUILayout.Button("Find Prefab"))
            {
                FindPrefabForSelectedGameObject();
            }

            prefabToReplaceWith = (GameObject)EditorGUILayout.ObjectField("Prefab to Replace With", prefabToReplaceWith, typeof(GameObject), false);

            if (prefabToReplaceWith != null && GUILayout.Button("Replace Selected GameObject with Prefab"))
            {
                ReplaceSelectedWithPrefab();
            }
        }
        else
        {
            GUILayout.Label("No GameObject selected!");
        }
    }

    private void FindPrefabForSelectedGameObject()
    {
        GameObject selectedGameObject = Selection.activeGameObject;

        if (selectedGameObject == null)
        {
            Debug.LogError("No GameObject selected!");
            return;
        }

        string prefabName = selectedGameObject.name;
        string[] guids = AssetDatabase.FindAssets(prefabName + " t:prefab");

        if (guids.Length == 0)
        {
            Debug.LogError("No prefab found with the name: " + prefabName);
            return;
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        prefabToReplaceWith = AssetDatabase.LoadAssetAtPath<GameObject>(path);

        if (prefabToReplaceWith == null)
        {
            Debug.LogError("Prefab could not be loaded from the path: " + path);
        }
    }

    private void ReplaceSelectedWithPrefab()
    {
        GameObject selectedGameObject = Selection.activeGameObject;

        if (selectedGameObject == null)
        {
            Debug.LogError("No GameObject selected!");
            return;
        }

        if (prefabToReplaceWith == null)
        {
            Debug.LogError("No prefab selected to replace with!");
            return;
        }

        ReplaceGameObjectWithPrefab(selectedGameObject, prefabToReplaceWith);
    }

    private void ReplaceGameObjectWithPrefab(GameObject gameObject, GameObject prefab)
    {
        Transform transform = gameObject.transform;
        GameObject newGameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab, transform.parent);

        newGameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        newGameObject.transform.localScale = transform.localScale;
        newGameObject.transform.SetSiblingIndex(transform.GetSiblingIndex());

        Undo.RegisterCreatedObjectUndo(newGameObject, "Replace With Prefab");
        Undo.DestroyObjectImmediate(gameObject);
    }
}
