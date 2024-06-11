using System;
using System.Collections;
using UnityEngine;

public class MapNotificationSystem : MonoBehaviour
{
    [SerializeField] private GameObject _notificationObject;

    private Map _map;

    private void Awake()
    {
        _notificationObject.SetActive(false);
    }
    private void OnEnable()
    {
        Location.OnLocationRevealed += NotifyOfLocationReveal;
        _map.OnMapOpened.AddListener(OnMapOpened);
    }

    private void OnDisable()
    {
        Location.OnLocationRevealed -= NotifyOfLocationReveal;
        _map.OnMapOpened.RemoveListener(OnMapOpened);
    }
    private void NotifyOfLocationReveal()
    {
        _notificationObject.SetActive(true);
    }

    private void OnMapOpened()
    {
        _notificationObject.SetActive(false);
    }
}