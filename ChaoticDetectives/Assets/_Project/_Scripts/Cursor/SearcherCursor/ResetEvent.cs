using UnityEngine;
using UnityEngine.Events;

public class ResetEvent : MonoBehaviour, IReset
{
    public UnityEvent resetEvent;
    public void Reset()
    {
        resetEvent?.Invoke();
    }
}