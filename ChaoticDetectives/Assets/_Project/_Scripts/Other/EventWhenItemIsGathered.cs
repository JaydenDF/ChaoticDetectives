using System;
using UnityEngine;
using UnityEngine.Events;

public class EventWhenItemIsGathered : MonoBehaviour
{
    public GameObject itemToCheck;
    public UnityEvent eventToTrigger;

    private void Awake()
    {
        Inventory.OnItemAddedWithGameObject += CheckIfItemIsGathered;
    }

    private void CheckIfItemIsGathered(GameObject otherItemToCheck)
    {
        if (otherItemToCheck == this.itemToCheck)
        {
            eventToTrigger.Invoke();
        }
    }
}