using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    List<Child> items = new List<Child>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsItemCollected()
    {
        foreach (Child child in items)
        {
        }

        return false;
    }
}

public class Child{

}
