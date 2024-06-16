using System;
using System.Collections;
using UnityEngine;

public class LoopMaster : MonoBehaviour {
    public static Action OnLooped;
    private LoopManager[] _loopManagers;

    private void Awake() {
        _loopManagers = FindObjectsOfType<LoopManager>(true);
    }

    [ContextMenu("Loop")]
    public void Loop() {
        OnLooped?.Invoke();
        foreach (var loopManager in _loopManagers) {
            loopManager.Loop();
        }
    }

    public void LoopAfterSeconds(float seconds) {
        StartCoroutine(LoopAfterSecondsCoroutine(seconds));
    }

    private IEnumerator LoopAfterSecondsCoroutine(float seconds) {
        yield return new WaitForSeconds(seconds);
        Loop();
    }
}