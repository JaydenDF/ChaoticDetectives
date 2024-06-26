using System;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnLoop : MonoBehaviour {
    public UnityEvent OnLooped;
    
    private void Awake() {
        LoopMaster.OnLooped += Loop;
    }

    private void OnDestroy() {
        LoopMaster.OnLooped -= Loop;
    }

    private void Loop()
    {
        OnLooped?.Invoke();
    }
}

