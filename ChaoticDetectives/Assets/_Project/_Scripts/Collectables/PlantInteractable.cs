using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractable : Interactable
{
    protected override void UseItem()
    {
        Debug.Log("im a plant!!!!!!!!!!!");
        neededItem.isUsed = true;
        currentState += 1;
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];
    }

    protected override void ApplyChangesNextLoop()
    {
        if(currentState < states.Count - 1)
        {
            Debug.Log("not fully grown yet");
            currentState += 1;
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];

        } else if(currentState == states.Count - 1)
        {
            Debug.Log("yeeeyyy you get an award!!!!!!!!!!!!");
        }
    }

    public void Apply()
    {
        ApplyChangesNextLoop();
    }
}
