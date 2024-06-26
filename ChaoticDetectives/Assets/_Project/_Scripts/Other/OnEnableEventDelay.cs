using UnityEngine;
using UnityEngine.Events;

public class OnEnableEventDelay : MonoBehaviour {
    [SerializeField] private bool _callEventOnEnable = false;
    public UnityEvent EventToCall;
    private void OnEnable() {
        if (_callEventOnEnable) {
            EventToCall.Invoke();
        }
    }

    public void SetCallEventOnEnable(bool value) {
        _callEventOnEnable = value;
    }

}