#if UNITY_EDITOR

namespace DialogueSystem
{
    using UnityEditor;

    using Object = UnityEngine.Object;

    public class DialogueContainerEditor : Editor
    {
        [UnityEditor.Callbacks.OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            // Get the object that was double-clicked
            Object obj = EditorUtility.InstanceIDToObject(instanceID);

            // Check if the object is of the correct type
            if (obj is DialogueContainer)
            {
                string nameOfAsset = obj.name;
                // Open the custom editor window
                DialogueGraph window = EditorWindow.GetWindow<DialogueGraph>();
                window.ShowWindow(nameOfAsset);
                return true; // Return true to indicate the event has been handled

            }

            return false; // Return false to let Unity handle other assets normally
        }
    }
}

#endif