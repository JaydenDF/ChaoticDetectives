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
        neededItem.UseItem();
        PlantPlanted?.Invoke();
        currentState = 1;
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
