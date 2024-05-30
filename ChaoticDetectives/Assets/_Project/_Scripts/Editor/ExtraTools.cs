using UnityEngine;
using UnityEditor;

public class SelectAndDisableChildrenEditor : EditorWindow
{
    private static bool isEnabled = false;

    [MenuItem("Window/Select And Disable Children %#d")] // Shortcut: Ctrl/Cmd + Shift + D
    public static void ShowWindow()
    {
        GetWindow<SelectAndDisableChildrenEditor>("Select And Disable Children");
    }

    [MenuItem("Window/Toggle Select And Disable Children %#t")] // Shortcut: Ctrl/Cmd + Shift + T
    public static void ToggleFunctionality()
    {
        isEnabled = !isEnabled;
        EditorWindow.GetWindow<SelectAndDisableChildrenEditor>().Repaint();
    }

    private void OnGUI()
    {
        GUILayout.Label("Toggle Select And Disable Children", EditorStyles.boldLabel);
        isEnabled = EditorGUILayout.Toggle("Enabled", isEnabled);
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;
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
}
