using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractable : Interactable
{
    //dirty for prototype
    public static Action PlantPlanted;
    protected override void UseItem()
    {
        for (int i = 0; i < neededItems.Count - 1; i++)
        {
            neededItems[i].neededItem.UseItem();
        }
        PlantPlanted?.Invoke();
        if (currentState < states.Count - 1)
        {
            currentState = 1;
        }
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];
    }

    protected override void ApplyChangesNextLoop()
    {
        if(currentState < states.Count - 1)
        {
            Debug.Log("lopper" +  currentState);
            currentState += 1;
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];
        }
    }
}
