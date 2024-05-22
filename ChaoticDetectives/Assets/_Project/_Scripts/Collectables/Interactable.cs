using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] protected Items neededItem;

    [SerializeField] private Inventory inventory;

    [SerializeField] protected List<Sprite> states = new List<Sprite>();
    [SerializeField] protected int currentState;

    private void OnEnable() {
        SimpleLoop.OnLooped += ApplyChangesNextLoop;
    }

    private void OnDisable() {
        SimpleLoop.OnLooped -= ApplyChangesNextLoop;
    }

    public void OnClick()
    {
        if (inventory.collectedItems.Contains(neededItem.transform.gameObject))
        {
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
        transform.gameObject.GetComponent<SpriteRenderer>().material = glowMaterial;
    }

    public void OnHoverExit()
    {
        transform.gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    protected virtual void UseItem()
    {
        //Code needed:
        //activate animation or what is needed. ask designers.
        //
        //Remove item from the inventory UI.
        neededItem.UseItem();
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
