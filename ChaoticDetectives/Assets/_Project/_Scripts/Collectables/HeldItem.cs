using UnityEngine;

public class HeldItem : MonoBehaviour
{
    private Interactable interactable;

    public GameObject parentUIItem = null;
    public bool hasCorrectItem;

    private AbstractInput _input;

    private void Awake()
    {
        _input = GetComponent<AbstractInput>();
    }

    private void OnEnable()
    {
        _input.OnClickDown += OnClick;
    }

    private void OnDisable()
    {
        _input.OnClickDown -= OnClick;
    }

    private void OnClick()
    {
        if(parentUIItem != null && !hasCorrectItem)
        {
            Destroy(gameObject);
            parentUIItem.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>())
        {
            interactable = collision.GetComponent<Interactable>();
            if(interactable.neededItem.GetComponent<SpriteRenderer>().sprite == transform.gameObject.GetComponent<SpriteRenderer>().sprite)
            {
                interactable.currentHeldItem = this;
                hasCorrectItem = true;
                
            } else
            {
                hasCorrectItem = false;
            }
        }
    }
}
