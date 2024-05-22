using System;
using UnityEngine;

public class SimpleLoop : MonoBehaviour
{
    public static Action OnLooped;
    [SerializeField] private GameObject _locationParent;
    [SerializeField] private GameObject _startLocation;

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