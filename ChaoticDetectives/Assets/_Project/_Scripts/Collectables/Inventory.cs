using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Items
    {
        public GameObject item;
        public bool hasBeenCollected;
    }

    public List<Items> inventory =  new List<Items>();
}
