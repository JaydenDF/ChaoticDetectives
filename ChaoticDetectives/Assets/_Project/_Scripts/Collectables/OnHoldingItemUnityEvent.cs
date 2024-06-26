using System;
using UnityEngine;
using UnityEngine.Events;

public class OnHoldingItemUnityEvent : MonoBehaviour {
    [SerializeField] private string _objectToDetect;
    public UnityEvent EventToCall;
    public UnityEvent EventOnRelease;

    private void Awake() {
        HeldItem.OnHoldingItem += OnHoldingItem;
        HeldItem.OnReleaseItem += OnReleaseItem;
    }

    private void OnDestroy() {
        HeldItem.OnHoldingItem -= OnHoldingItem;
        HeldItem.OnReleaseItem -= OnReleaseItem;
    }

    private void OnHoldingItem(string name)
    {
        if (name == _objectToDetect)
        {
            EventToCall.Invoke();
        }
    }

    private void OnReleaseItem(string name)
    {
        if (name == _objectToDetect)
        {
            EventOnRelease.Invoke();
        }
    }
}