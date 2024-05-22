using System;
using UnityEngine;

public class LoopAnimation : MonoBehaviour {
    [SerializeField] private Animator _animator;
    private void Awake() {
        _animator = GetComponent<Animator>();
    }
    private void OnEnable() {
        LoopWhen.OnLooped += PlayAnimation;
    }

    private void OnDisable() {
        LoopWhen.OnLooped -= PlayAnimation;
    }

    [ContextMenu("Play Animation")]
    private void PlayAnimation()
    {
        _animator.SetTrigger("Loop");
    }
    
}