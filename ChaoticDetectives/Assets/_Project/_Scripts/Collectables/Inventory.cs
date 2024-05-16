using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public List<GameObject> inventory =  new List<GameObject>();
    public List<GameObject> collectedItems = new List<GameObject>();


    private void Update()
    {
        Debug.Log(collectedItems.Count);
    }
}
