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
        Debug.Log("has looped");
        if(currentState < states.Count - 1)
        {
            currentState += 1;
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];

        } else if(currentState == states.Count - 1)  //fully grown
        {
           
        }
    }
}
