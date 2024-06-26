using UnityEngine;
using UnityEngine.Events;

using Image = UnityEngine.UI.Image;
public class ButtonSelectionHandler : MonoBehaviour, IInteractable
{
    public UnityEvent OnClicked;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color unselectedColor;
    [SerializeField][Range(0, 2)] private float scaleMultiplier = 1.1f;

    private Image _image;
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _image = GetComponent<Image>();
        _image.color = unselectedColor;
    }
    public void OnClick()
    {
        OnClicked.Invoke();
    }

    public void OnHoverEnter()
    {
        transform.localScale *= scaleMultiplier;
        _image.color = selectedColor;
    }

    public void OnHoverExit()
    {
        transform.localScale = _originalScale;
        _image.color = unselectedColor;
    }
}