using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrow : MonoBehaviour
{
    public PlantInteractable interactable;

    public void NextLoop()
    {
        interactable.Apply();
    }
}
