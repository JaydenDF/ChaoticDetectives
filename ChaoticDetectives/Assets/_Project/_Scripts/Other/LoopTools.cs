using UnityEditor;
using UnityEngine;

public class LoopTools : EditorWindow
{
    private int loopIndex = 0;

    [MenuItem("Tools/Loop Tools")]
    public static void ShowWindow()
    {
        GetWindow<LoopTools>("Loop Tools");
    }

    private void OnGUI()
    {
        GUILayout.Label("Loop Manager Controls", EditorStyles.boldLabel);

        loopIndex = EditorGUILayout.IntField("Loop Index", loopIndex);

        if (GUILayout.Button("Enable Loop"))
        {
            EnableLoopInAllManagers(loopIndex);
        }
    }

    private void EnableLoopInAllManagers(int index)
    {
        LoopManager[] loopManagers = FindObjectsOfType<LoopManager>(true);

        foreach (LoopManager manager in loopManagers)
        {
            manager.EnableLoop(index);
        }

        Debug.Log("Enabled loop " + index + " in all LoopManagers.");
    }
}
