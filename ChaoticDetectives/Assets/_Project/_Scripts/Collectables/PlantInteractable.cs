using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractable : Interactable
{
    protected override void UseItem()
    {
        neededItem.isUsed = true;
        currentState += 1;
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];
    }

    protected override void ApplyChangesNextLoop()
    {
        if(currentState < states.Count - 1)
        {
            currentState += 1;
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];

        } else if(currentState == states.Count - 1)  //fully grown
        {
           
        }
    }

    public void Apply()
    {
        ApplyChangesNextLoop();
    }
}
