
// the cursor looks for interactable objects and calls these functions when the cursor is over them
// make sure that you object has a collider!
public interface IInteractable
{
    void OnClick();
    void OnHoverEnter();
    void OnHoverExit();
}
