using System;
using UnityEngine;

public class Location : MonoBehaviour, IInteractable
{

    public static Action<GameObject> OnLocationClicked;
    public static Action OnLocationRevealed;

    public bool Revealed => _revealed;
    [SerializeField] private bool _revealed = false;
    [SerializeField] private GameObject _locationObject;

    private const float _ammountToInecreaseSize = 1.1f;
    private Vector3 _initialSize;
    [SerializeField] private bool _wasRevealedFromStart = false;

    private void Awake()
    {
        _wasRevealedFromStart = _revealed;
        _initialSize = this.transform.localScale;

        LoopMaster.OnLooped += Reset;
    }
    private void OnDestroy()
    {
        LoopMaster.OnLooped -= Reset;
    }

    private void Reset()
    {
        _revealed = _wasRevealedFromStart;
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

    public void Reveal(bool shouldReveal)
    {
        if (shouldReveal) { OnLocationRevealed?.Invoke(); }
        _revealed = shouldReveal;
    }

    public void ChangeLocation(GameObject newLocation)
    {
        _locationObject = newLocation;
    }
}