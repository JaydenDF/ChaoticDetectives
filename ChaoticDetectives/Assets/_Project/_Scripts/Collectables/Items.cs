using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour, IInteractable
{
    public static Action OnCollected;
    public bool isCollected;
    private bool isUsed;

    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private Inventory inventory;
    [SerializeField] private Collider2D _colliderToDisable;
    
    public GameObject inventorySlotPrefab;
    public GameObject inventoryPanel;

    private GameObject instantiatedPrefab;
    private UIItem instantiatedPrefabUI;

    private void Awake()
    {
        isCollected = false;
        isUsed = false;
    }
    private void OnEnable()
    {
        SimpleLoop.OnLooped += ResetItem;
    }

    private void ResetItem()
    {
        isCollected = false;
        isUsed = false;
        gameObject.GetComponent<Renderer>().enabled = true;
        if (_colliderToDisable != null) { _colliderToDisable.enabled = true; }
    }

    private void Update()
    {
        if (isCollected)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public void OnClick()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory is not assigned!");
            return;
        }

        if (!inventory.collectedItems.Contains(gameObject))
        {
            OnCollected?.Invoke();
            isCollected = true;
            inventory.AddToInventory(this.gameObject);
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

    public void UseItem()
    {
        Debug.Log("Item used!");
        isUsed = true;

        if (isUsed == false) { return; }

        inventory.collectedItems.Remove(gameObject);
    }
}
