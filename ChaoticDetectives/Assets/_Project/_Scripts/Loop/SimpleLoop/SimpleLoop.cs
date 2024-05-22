using System;
using UnityEngine;

public class SimpleLoop : MonoBehaviour
{
    public Action OnLooped;
    [SerializeField] private GameObject _locationParent;
    [SerializeField] private GameObject _startLocation;

    [ContextMenu("Loop")]
    public void Loop()
    {
        OnLooped?.Invoke();

        //DISABLE CHILDREN OF LOCA
        foreach (Transform child in _locationParent.transform)
        {
            child.gameObject.SetActive(false);
        }

        _startLocation.SetActive(true);
    }
}