using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EventOnClicked : MonoBehaviour, IInteractable
{
    public UnityEvent _eventOnTriggered;
    public void OnClick()
    {
        _eventOnTriggered.Invoke();
    }

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }
}