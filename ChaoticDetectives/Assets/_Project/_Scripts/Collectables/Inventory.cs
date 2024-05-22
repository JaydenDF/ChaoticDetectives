using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private void OnEnable()
    {
        SimpleLoop.OnLooped += ClearInventory;
    }

    private void OnDisable()
    {
        SimpleLoop.OnLooped -= ClearInventory;
    }

    public List<GameObject> collectedItems = new List<GameObject>();
    public List<GameObject> UIStorage = new List<GameObject>();

    private void ClearInventory()
    {
        collectedItems.Clear();
        UIStorage.Clear();
    }
}
