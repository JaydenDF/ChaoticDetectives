using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour, IInteractable
{
    public static Action OnCollected;
    public bool isCollected;
    private bool isUsed;

    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material defaultMaterial;

    private Inventory inventory;
    private Collider2D _colliderToDisable;

    public GameObject inventorySlotPrefab;
    public GameObject inventoryHolder;


    public string itemDesc;

    public string itemMonologueText;
    private GameObject itemMonologueUIText;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();

        isCollected = false;
        isUsed = false;
        inventory = FindObjectOfType<Inventory>();
        
        var collider = GetComponents<Collider2D>();
        foreach (var col in collider)
        {
            if (col.isTrigger == false) 
            {
                _colliderToDisable = col;
            }
        }
        itemMonologueUIText = GameObject.Find("MonologueText");

        inventoryHolder = GameObject.Find("InventoryHolder");
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
            CollectItem();
        }

        _colliderToDisable.enabled = false;
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
        isUsed = true;

        if (isUsed == false) { return; }

        inventory.collectedItems.Remove(gameObject);
    }

    public void CollectItem()
    {
        OnCollected?.Invoke();
        isCollected = true;

        if(inventorySlotPrefab == null) {inventory = FindObjectOfType<Inventory>();}
        if(itemMonologueUIText == null) {itemMonologueUIText = GameObject.Find("MonologueText");}
        itemMonologueUIText.GetComponent<TMP_Text>().SetText(itemMonologueText);
        inventory.AddToInventory(this.gameObject);
    }
}
