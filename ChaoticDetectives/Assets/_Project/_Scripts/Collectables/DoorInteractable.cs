using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    protected override void UseItem()
    {
        Debug.Log("im a door!!!!!!!!!!!");
    }

    protected override void ApplyChangesNextLoop()
    {
        base.ApplyChangesNextLoop();
    }
}
