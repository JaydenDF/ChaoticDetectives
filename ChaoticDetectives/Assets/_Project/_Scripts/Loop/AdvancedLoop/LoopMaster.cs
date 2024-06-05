using UnityEngine;

public class LoopMaster : MonoBehaviour {
    private LoopManager[] _loopManagers;

    private void Awake() {
        _loopManagers = FindObjectsOfType<LoopManager>(true);
    }

    [ContextMenu("Loop")]
    public void Loop() {
        foreach (var loopManager in _loopManagers) {
            loopManager.Loop();
        }
    }
}