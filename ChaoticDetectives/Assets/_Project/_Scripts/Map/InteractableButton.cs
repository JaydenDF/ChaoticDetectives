using UnityEngine;
using UnityEngine.Events;

public class InteractableButton : MonoBehaviour, IInteractable
{
    public UnityEvent OnClicked;
    public UnityEvent OnHovered;
    public UnityEvent OnHoverExited;
    public void OnClick()
    {
        OnClicked.Invoke();
    }

    public void OnHoverEnter()
    {
        OnHovered.Invoke();
    }

    public void OnHoverExit()
    {
        OnHoverExited.Invoke();
    }
}