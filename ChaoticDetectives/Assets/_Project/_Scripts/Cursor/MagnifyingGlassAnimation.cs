using UnityEngine;

public class MagnifyingGlassAnimation : MonoBehaviour {
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }
    public void Animate() {
        _animator.SetTrigger("Magnify");
    }
}