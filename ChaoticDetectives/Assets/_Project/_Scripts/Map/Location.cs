using System;
using UnityEngine;

public class Location : MonoBehaviour, IInteractable
{
    public bool Revealed = false;
    public static Action<GameObject> OnLocationClicked;
    [SerializeField] private GameObject _locationObject;
    private const float _ammountToInecreaseSize = 1.1f;
    private Vector3 _initialSize;

    private void Awake()
    {
        _initialSize = this.transform.localScale;
    }
    public bool IsLocation(GameObject @object)
    {
        return _locationObject == @object;
    }

    public void DisableObject()
    {
        _locationObject.SetActive(false);
    }

    public void EnableObject()
    {
        _locationObject.SetActive(true);
    }

    public void OnClick()
    {
        OnLocationClicked?.Invoke(_locationObject);
    }

    public void OnHoverEnter()
    {
        this.transform.localScale = _initialSize * _ammountToInecreaseSize;
    }

    public void OnHoverExit()
    {
        this.transform.localScale = _initialSize;
    }
}