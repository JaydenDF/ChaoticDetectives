using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IReset
{
    public UnityEvent OnItemAdded;
    public static Action<GameObject> OnItemAddedWithGameObject;
    public GameObject inventorySlotPrefab;
    public GameObject inventoryPanel;
    private GameObject instantiatedPrefab;
    private UIItem instantiatedPrefabUIScript;

    private Items itemScript;


    private void OnEnable()
    {
        LoopMaster.OnLooped += ClearInventory;
    }

    private void OnDisable()
    {
        LoopMaster.OnLooped -= ClearInventory;
    }

    public List<GameObject> collectedItems = new List<GameObject>();
    public List<GameObject> UIStorage = new List<GameObject>();

    private void ClearInventory()
    {
        collectedItems.Clear();
        UIStorage.Clear();
    }

    private void AddItemToInventoryList(GameObject item)
    {
        collectedItems.Add(item);
    }

    private void AddUIToInventory(GameObject instantiatedPrefab)
    {
        UIStorage.Add(instantiatedPrefab);
    }

    //Adding Item to inventory
    //AND
    //Adding changing the sprite of the prefab to the sprite of the image.
    public void AddToInventory(GameObject item)
    {
        EventOnItemAdded(item);
        AddItemToInventoryList(item);
        inventorySlotPrefab.transform.gameObject.GetComponent<Image>().sprite = item.transform.gameObject.GetComponent<SpriteRenderer>().sprite;
        instantiatedPrefab = Instantiate(inventorySlotPrefab, parent: inventoryPanel.transform);
        instantiatedPrefab.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        instantiatedPrefabUIScript = instantiatedPrefab.GetComponent<UIItem>();
        itemScript = item.GetComponent<Items>();
        instantiatedPrefabUIScript.parentItem = item;
        AddUIToInventory(instantiatedPrefab);
    }

    public void RemoveFromInventory(GameObject item)
    {
        if (collectedItems.Contains(item) == false){return;}
        collectedItems.Remove(item);
        UIStorage.Remove(item);
    }

    private void EventOnItemAdded(GameObject item)
    {
        OnItemAdded.Invoke();
        if (OnItemAddedWithGameObject != null)
        {
            OnItemAddedWithGameObject.Invoke(item);
        }
    }

    public void Reset()
    {
        ClearInventory();
    }
}
