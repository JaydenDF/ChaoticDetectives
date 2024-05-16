using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour, IInteractable
{
    public bool isCollected;
    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Inventory inventory;

    private ItemSO itemSO;
    public GameObject inventorySlotPrefab;
    public GameObject inventoryPanel;


    private void Awake()
    {
        isCollected = false;
        itemSO = GetComponent<ItemSO>();
    }

    public void OnClick()
    {
        if (inventory != null && !inventory.collectedItems.Contains(gameObject))
        {
            isCollected= true;
            inventory.collectedItems.Add(gameObject);
            inventorySlotPrefab.transform.gameObject.GetComponent<Image>().sprite = transform.gameObject.GetComponent<SpriteRenderer>().sprite;
            Instantiate(inventorySlotPrefab, parent: inventoryPanel.transform);
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
