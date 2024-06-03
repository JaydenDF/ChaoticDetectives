using UnityEngine;

public interface IHighlitable
{
    void OnHoverEnter();
    void OnHoverExit();

}
public class Highlitable : MonoBehaviour, IHighlitable
{
    [SerializeField] private Color HighliteColor = Color.red; 
    private Collider2D _triggerCollider;
    private Color _defaultColor;
    //copy of _interactable

    private void Awake()
    {
        var arr = GetComponents<Collider2D>();
        //get the trigger
        foreach (var col in arr)
        {
            if (col.isTrigger)
            {
                _triggerCollider = col;
            }
        }

        _defaultColor = GetComponent<SpriteRenderer>().color;
        _triggerCollider.enabled = false;
    }
    public void OnHoverEnter()
    {
        GetComponent<SpriteRenderer>().color = HighliteColor;
        _triggerCollider.enabled = true;
    }

    public void OnHoverExit()
    {
        GetComponent<SpriteRenderer>().color = _defaultColor;
        _triggerCollider.enabled = false;
    }
}