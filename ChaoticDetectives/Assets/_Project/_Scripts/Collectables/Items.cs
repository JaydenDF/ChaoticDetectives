using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour, IInteractable
{
    public bool isCollected;
    private bool isUsed;

    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private Inventory inventory;

    public GameObject inventorySlotPrefab;
    public GameObject inventoryPanel;

    private GameObject instantiatedPrefab;
    private UIItem instantiatedPrefabUI;
    private void Awake()
    {
        isCollected = false;
        isUsed = false;
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
            isCollected = true;
            inventory.collectedItems.Add(gameObject);
            inventorySlotPrefab.transform.gameObject.GetComponent<Image>().sprite = transform.gameObject.GetComponent<SpriteRenderer>().sprite;
            instantiatedPrefab = Instantiate(inventorySlotPrefab, parent: inventoryPanel.transform);
            instantiatedPrefabUI = instantiatedPrefab.GetComponent<UIItem>();
            instantiatedPrefabUI.parentItem = gameObject;
            inventory.UIStorage.Add(instantiatedPrefab);
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
