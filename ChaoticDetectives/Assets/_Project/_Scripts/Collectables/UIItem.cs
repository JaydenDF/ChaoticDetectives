using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IInteractable
{
    public GameObject parentItem;
    public Inventory inventory;
    public Sprite itemSprite = null;

    public int itemIndex;
    private bool isAdded;

    private GameObject cursor;
    public GameObject itemPrefab;
    public HeldItem itemPrefabScript;

    public string ItemDesc;

    [SerializeField] private GameObject ItemDescImage;
    public GameObject ItemDescText;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        transform.gameObject.GetComponent<Image>().sprite = itemSprite;
        isAdded = false;
        cursor = GameObject.Find("Cursor(Searcher)");
        ItemDescImage = GameObject.Find("ItemDescImage");
        ItemDescText = GameObject.Find("ItemDescText");
    }

    private void Update()
    {
        if (!inventory.collectedItems.Contains(parentItem))
        {
            inventory.UIStorage.Remove(gameObject);
        }

        if (!inventory.UIStorage.Contains(gameObject))
        {
            Destroy(gameObject);
        }

        SetIndex();
    }

    private void SetIndex()
    {
        if (isAdded == false)
        {
            itemIndex = inventory.UIStorage.Count;
            isAdded = true;
        }

    }

    public void OnClick()
    {
        itemPrefab.transform.gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<Image>().sprite;
        itemPrefabScript = itemPrefab.GetComponent<HeldItem>();
        itemPrefabScript.parentUIItem = gameObject;
        Instantiate(itemPrefab, parent: cursor.transform);
        gameObject.SetActive(false);
    }

    public void OnHoverEnter()
    {
        ItemDescText.GetComponent<TMP_Text>().SetText(ItemDesc);
        ItemDescImage.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
    }

    public void OnHoverExit()
    {

    }
}
