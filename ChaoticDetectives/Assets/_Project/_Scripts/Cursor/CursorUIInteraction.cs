using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class CursorUIInteraction : MonoBehaviour, IInteractable
{
    public RectTransform uiElement;

    public GameObject itemPrefab;

    private bool isHoldingObject;

    private Collider2D _collision;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        _collision = collision;
        if (collision.gameObject.GetComponent<UIItem>() && !isHoldingObject)
        {
            //itemPrefab.transform.gameObject.GetComponent<SpriteRenderer>().sprite = collision.transform.gameObject.GetComponent<Image>().sprite;
            //Instantiate(itemPrefab, parent: transform);
            isHoldingObject = true;
        }
    }

    public void OnClick()
    {
        Debug.Log(gameObject);
    }

    public void OnHoverEnter()
    {

    }

    public void OnHoverExit()
    {
        
    }
}
