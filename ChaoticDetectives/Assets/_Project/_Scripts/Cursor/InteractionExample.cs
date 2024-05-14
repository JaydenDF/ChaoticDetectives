using UnityEngine;

// the cursor looks for interactable objects and calls these functions when the cursor is over them
// make sure that you object has a collider!
[RequireComponent(typeof(Collider2D))]
public class InteractionExample : MonoBehaviour, IInteractable
{
    public float sizeToGrow = 1.1f;
    private Vector3 originalScale;
    private void Awake() {
        originalScale = transform.localScale;
    }
    public void OnClick()
    {
        Debug.Log("Clicked!");
    }

    public void OnHoverEnter()
    {
        transform.localScale = originalScale * sizeToGrow;
        Debug.Log("Hovering!");
    }

    public void OnHoverExit()
    {
        transform.localScale = originalScale;
        Debug.Log("Not hovering!");
    }
}
