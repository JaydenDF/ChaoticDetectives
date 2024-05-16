using UnityEngine;
using UnityEngine.Events;

public class InteractableButton : MonoBehaviour, IInteractable
{
    public UnityEvent OnClicked;
    public void OnClick()
    {
        OnClicked.Invoke();
    }

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }
}