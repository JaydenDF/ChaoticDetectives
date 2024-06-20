using UnityEngine;

public class BecomeSmaller : MonoBehaviour {
    [SerializeField] private float _scaleBy = 0.1f;

    private Vector3 _originalScale;

    private void Awake() {
        _originalScale = transform.localScale;
    }

    public void Shrink() {
        transform.localScale = _originalScale / _scaleBy;
    }

    public void Grow() {
        transform.localScale = _originalScale / _scaleBy;
    }

    public void Reset() {
        transform.localScale = _originalScale;
    }
}