using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public UnityEvent OnItemAdded;
    public static UnityEvent<GameObject> OnItemAddedWithGameObject;
    public GameObject inventorySlotPrefab;
    public GameObject inventoryPanel;
    private GameObject instantiatedPrefab;
    private UIItem instantiatedPrefabUIScript;


    private void OnEnable()
    {
        SimpleLoop.OnLooped += ClearInventory;
    }

    private void OnDisable()
    {
        SimpleLoop.OnLooped -= ClearInventory;
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

    public void AddToInventory(GameObject item)
    {
        AddItemToInventoryList(item);
        instantiatedPrefab = Instantiate(inventorySlotPrefab, parent: inventoryPanel.transform);
        instantiatedPrefab.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        instantiatedPrefabUIScript = instantiatedPrefab.GetComponent<UIItem>();
        instantiatedPrefabUIScript.parentItem = item;
        AddUIToInventory(instantiatedPrefab);
        EventOnItemAdded(item);
    }

    private void EventOnItemAdded(GameObject item)
    {
        OnItemAdded.Invoke();
        if (OnItemAddedWithGameObject != null)
        {
            OnItemAddedWithGameObject.Invoke(item);
        }
    }
}
