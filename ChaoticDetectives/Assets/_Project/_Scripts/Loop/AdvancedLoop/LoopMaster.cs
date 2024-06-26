using System;
using System.Collections;
using UnityEngine;

public class LoopMaster : MonoBehaviour, IReset 
{
    public static Action OnLooped;
    private LoopManager[] _loopManagers;

    [SerializeField] private int _startOnLoop = 0;

    private void Awake() {
        _loopManagers = FindObjectsOfType<LoopManager>(true);
    }

    private void Start() {
        if (_startOnLoop > 0) {
            for (int i = 0; i < _startOnLoop; i++) {
                Loop();
            }
        }
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

    public void Reset() {
        foreach (var loopManager in _loopManagers) {
            loopManager.ResetLoops();
        }
    }
}