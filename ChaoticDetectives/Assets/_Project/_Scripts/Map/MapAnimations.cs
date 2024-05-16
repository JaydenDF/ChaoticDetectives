using UnityEngine;

public class MapAnimations : MonoBehaviour {
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void Show() {
        _animator.SetBool("Show", true);
    }

    public void Hide() {
        _animator.SetBool("Show", false);
    }
}