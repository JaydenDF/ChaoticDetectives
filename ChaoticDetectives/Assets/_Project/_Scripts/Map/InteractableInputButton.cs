using UnityEngine;

using Object = UnityEngine.Object;
public class InteractableInputButton : InteractableButton {
    [SerializeField] private AbstractInput _input;
    private void Awake() {
        _input = GetComponent<AbstractInput>();
    }
    private void OnEnable() {
        _input.OnClickDown += OnClick;
    }

    private void OnDisable() {
        _input.OnClickDown -= OnClick;
    }
}