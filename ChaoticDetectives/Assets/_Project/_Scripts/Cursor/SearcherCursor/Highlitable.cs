using UnityEditor.SceneManagement;
using UnityEngine;

public interface IHighlitable
{
    void OnHoverEnter();
    void OnHoverExit();

}
public class Highlitable : MonoBehaviour, IHighlitable
{
    [SerializeField] private Color HighliteColor = Color.red;
    [SerializeField] private Collider2D _triggerCollider;
    private Color _defaultColor;
    //copy of _interactable

    private void Awake()
    {
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