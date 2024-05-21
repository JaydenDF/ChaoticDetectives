using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractable : Interactable
{
    protected override void UseItem()
    {
        Debug.Log("im a plant!!!!!!!!!!!");
        neededItem.isUsed = true;
    }

    protected override void ApplyChangesNextLoop()
    {
        if(currentState < states.Count - 1)
        {
            Debug.Log("not fully grown yet");
        } else if(currentState == states.Count - 1)
        {
            Debug.Log("yeeeyyy you get an award!!!!!!!!!!!!");
        }
    }
}
