using UnityEngine;
using UnityEngine.Events;

public class EventOnInteracted : Interactable
{
    public UnityEvent eventToTrigger;
    protected override void UseItem()
    {
        Debug.Log("EventOnInteracted");
        eventToTrigger.Invoke();
    }

}