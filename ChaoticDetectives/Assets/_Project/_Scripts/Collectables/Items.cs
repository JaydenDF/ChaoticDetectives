using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour, IInteractable
{
    public bool isCollected;
    public bool isUsed;

    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private Inventory inventory;

    public GameObject inventorySlotPrefab;
    public GameObject inventoryPanel;

    private GameObject instantiatedPrefab;
    private UIItem instantiatedPrefabUI;

    private void OnEnable()
    {
        LoopManager.OnLooped += NameOfFicntion;
    }

    private void OnDisable()
    {
        LoopManager.OnLooped -= NameOfFicntion;
    }

    private void NameOfFicntion()
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        isCollected = false;
        isUsed = false;
    }

    private void Update()
    {
        if(isCollected) 
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }

        if(isUsed)
        {
            inventory.collectedItems.Remove(gameObject);
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
            isCollected= true;
            inventory.collectedItems.Add(gameObject);
            inventorySlotPrefab.transform.gameObject.GetComponent<Image>().sprite = transform.gameObject.GetComponent<SpriteRenderer>().sprite;
            instantiatedPrefab = Instantiate(inventorySlotPrefab, parent: inventoryPanel.transform);
            instantiatedPrefabUI = instantiatedPrefab.GetComponent<UIItem>();
            instantiatedPrefabUI.parentItem = gameObject;
            inventory.UIStorage.Add(instantiatedPrefab);
            Debug.Log("im not in the list yet!!!!!!!!!");
        }
        Debug.Log("Clicked!");
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
}
