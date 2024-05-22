using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    public GameObject parentItem;

    public Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (!inventory.collectedItems.Contains(parentItem))
        {
            inventory.UIStorage.Remove(gameObject);
        }

        if(!inventory.UIStorage.Contains(gameObject)) 
        { 
            Object.Destroy(gameObject);
        }
    }
}
