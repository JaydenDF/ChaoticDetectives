using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Interactable;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material defaultMaterial;

    private Inventory inventory;

    [SerializeField] protected List<Sprite> states = new List<Sprite>();
    [SerializeField] protected int currentState;

    private SpriteRenderer spriteRenderer;

    public HeldItem currentHeldItem = null;

    [Serializable]
    public class NeededItems
    {
        public Items neededItem;
        public bool hasCollectedThisItem;
    }

    public List<NeededItems> neededItems;

    private void OnEnable()
    {
        SimpleLoop.OnLooped += ApplyChangesNextLoop;
    }

    private void OnDisable()
    {
        SimpleLoop.OnLooped -= ApplyChangesNextLoop;
    }

    public void OnClick()
    {
        if (currentHeldItem == null) { return; }

        if (currentHeldItem.hasCorrectItem == false)
        {
            Destroy(currentHeldItem.transform.gameObject);
            currentHeldItem.parentUIItem.gameObject.SetActive(true);
        }
        else if (currentHeldItem.hasCorrectItem == true)
        {

            SetNeededItemsBoolToTrue();
        }
    }

    private void Update()
    {
        CheckIfAllNeededItemsAreCollected();
    }

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        if (GetComponent<SpriteRenderer>() != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (states.Count > 0 && currentState == 0)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];
        }
    }

    public void OnHoverEnter()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.material = glowMaterial;
        }
    }

    public void OnHoverExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.material = defaultMaterial;
        }
    }

    protected virtual void UseItem()
    {
        for (int i = 0; i < neededItems.Count; i++)
        {
            Debug.Log(neededItems[i].neededItem);
        }

        if (currentState < states.Count - 1)
        {
            currentState = 1;
        }
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = states[currentState];
    }

    protected virtual void ApplyChangesNextLoop()
    {
        if (currentState < states.Count - 1)
        {
            currentState = 1;
        }
    }

    private void SetNeededItemsBoolToTrue()
    {
        for (int i = 0; i < neededItems.Count; i++)
        {
            if (currentHeldItem.gameObject.GetComponent<SpriteRenderer>().sprite == neededItems[i].neededItem.gameObject.GetComponent<SpriteRenderer>().sprite)
            {
                neededItems[i].hasCollectedThisItem = true;
                neededItems[i].neededItem.UseItem();
                break;
            }
        }
    }

    private void CheckIfAllNeededItemsAreCollected()
    {

        for (int i = 0; i < neededItems.Count; i++)
        {
            if (neededItems[i].hasCollectedThisItem == false)
            {
                break;
            }
            else if (neededItems[i].hasCollectedThisItem && i == neededItems.Count - 1)
            {
                UseItem();
            }
        }
    }
}
