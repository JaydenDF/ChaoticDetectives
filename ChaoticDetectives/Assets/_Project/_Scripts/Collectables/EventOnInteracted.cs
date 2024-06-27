using UnityEngine;
using UnityEngine.Events;

public class EventOnInteracted : Interactable, IReset
{
    public UnityEvent eventToTrigger;
    private bool _hasBeenClicked = false;
    protected override void UseItem()
    {
        if (_hasBeenClicked) return;

        eventToTrigger.Invoke();
        _hasBeenClicked = true;
    }

    public new void Reset()
    {
        base.Reset();

        _hasBeenClicked = false;

        if(GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

}