using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifferentEventsForDifferentItems : Interactable {
    [SerializeField] private List<UnityEvent> DifferentEventsForDifferentItemsList;
    private UnityEvent _eventToTrigger => DifferentEventsForDifferentItemsList[_eventIndex];
    private int _eventIndex;

    protected override void SetNeededItemsBoolToTrue()
    {
        for (int i = 0; i < neededItems.Count; i++)
        {
            if (currentHeldItem.gameObject.GetComponent<SpriteRenderer>().sprite == neededItems[i].neededItem.gameObject.GetComponent<SpriteRenderer>().sprite)
            {
                neededItems[i].hasCollectedThisItem = true;
                neededItems[i].neededItem.UseItem();

                _eventIndex = i;     

                _eventToTrigger.Invoke();        
                break;
            }
        }
    }
}

