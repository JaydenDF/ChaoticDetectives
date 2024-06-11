using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneObjectMappings", menuName = "Scene Object Mappings")]
public class SceneObjectMappings : ScriptableObject
{
    public List<SceneMapping> sceneMappings = new List<SceneMapping>();

    [System.Serializable]
    public class SceneMapping
    {
        public string sceneName;
        public List<string> gameObjectNames;
    }
}

