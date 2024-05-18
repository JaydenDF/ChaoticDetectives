using UnityEngine;

public interface IHighlitable
{
    void OnHoverEnter();
    void OnHoverExit();

}
public class Highlitable : MonoBehaviour, IHighlitable
{
    [SerializeField] private Color HighliteColor = Color.red;
    private Color _defaultColor;

    private void Awake() {
        _defaultColor = GetComponent<SpriteRenderer>().color;
    }
    public void OnHoverEnter()
    {
        GetComponent<SpriteRenderer>().color = HighliteColor;
    }

    public void OnHoverExit()
    {
        GetComponent<SpriteRenderer>().color = _defaultColor;
    }
}