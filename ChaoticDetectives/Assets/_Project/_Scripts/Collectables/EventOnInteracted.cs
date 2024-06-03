using UnityEngine;
using UnityEngine.Events;

public class EventOnInteracted : Interactable
{
    public UnityEvent eventToTrigger;
    private bool _hasBeenClicked = false;
    protected override void UseItem()
    {
        if (_hasBeenClicked) return;

        eventToTrigger.Invoke();
        _hasBeenClicked = true;
    }

}