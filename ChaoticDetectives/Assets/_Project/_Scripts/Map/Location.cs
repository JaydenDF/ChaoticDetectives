using System;
using System.IO.Compression;
using System.Linq;
using UnityEngine;

public class Location : MonoBehaviour, IInteractable, IReset
{

    public static Action<GameObject> OnLocationClicked;
    public static Action OnLocationRevealed;

    public bool Revealed => _revealed;
    [SerializeField] private bool _revealed = false;
    [SerializeField] private string _otherLocationName;
    private const float _ammountToInecreaseSize = 1.1f;
    private Vector3 _initialSize;
    [SerializeField] private bool _wasRevealedFromStart = false;

    private void Awake()
    {
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



    public void DisableObject()
    {
        GameObject _locationObject = FindInactiveObjectByName(_otherLocationName);

        if (_locationObject == null) { return; }
        _locationObject.SetActive(false);
    }

    public void EnableObject()
    {
        GameObject _locationObject = FindInactiveObjectByName(_otherLocationName);

        if (_locationObject == null) { return; }

        _locationObject.SetActive(true);
    }

    public void OnClick()
    {
        var _locationObject = FindInactiveObjectByName(_otherLocationName);

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


    public bool IsLocation(GameObject locationObject)
    {
        bool result = false;
        if (locationObject == null) { return false; }
        string name = locationObject.name;
        if (name == _otherLocationName)
        {
            Debug.Log("TRUE");
            result = true;
        }

        return result;
    }

    private GameObject FindInactiveObjectByName(string name)
    {
        var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        GameObject @object = allObjects.FirstOrDefault(obj => obj.name == name);
        return @object;
    }

    void IReset.Reset()
    {
        Reset();
    }
}