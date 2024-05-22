using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material defaultMaterial;

    protected Items neededItem;

    [SerializeField] private Inventory inventory;

    [SerializeField] protected List<Sprite> states = new List<Sprite>();
    protected int currentState;

    public void OnClick()
    {
        if (inventory.collectedItems.Contains(neededItem.transform.gameObject))
        {
            Debug.Log("Interacted");
            UseItem();
            
        } 
    }

    private void Awake()
    {
        if(states.Count > 0 && currentState == 0) 
        {
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];
        }
    }

    public void OnHoverEnter()
    {
        Debug.Log("Hovering!");
        transform.gameObject.GetComponent<SpriteRenderer>().material = glowMaterial;
    }

    public void OnHoverExit()
    {
        Debug.Log("Not hovering!");
        transform.gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    protected virtual void UseItem()
    {
        Debug.Log("Im the basic Bitch!!!!!!!!");
        //Code needed:
        //activate animation or what is needed. ask designers.
        //
        //Remove item from the inventory UI.
        neededItem.isUsed = true;
        currentState += 1;
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];

    }

    protected virtual void ApplyChangesNextLoop()
    {
        Debug.Log("I changed in the next Loop!!!!!!!!");
        currentState += 1;
        //subscribe to loop
    }
}
