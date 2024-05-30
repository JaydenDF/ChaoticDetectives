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

    void Update()
    {
        Vector2 cursorPos = transform.position;
        Vector2 localCursorPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiElement, cursorPos, Camera.main, out localCursorPos);

        //bool condition = uiElement.rect.Contains(cursorPos);
        if (uiElement.rect.Contains(localCursorPos))
        {
            // UI Element Activation Code
            ActivateUIElement();
        }
        else
        {
            // Optional: Deactivate or reset the UI element if needed
            DeactivateUIElement();
        }
    }

    void ActivateUIElement()
    {
        // Put activation logic here
        
    }

    void DeactivateUIElement()
    {
        // Put deactivation logic here
        
    }

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
