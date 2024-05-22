using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map : MonoBehaviour
{
    List<Location> locations = new List<Location>();
    [SerializeField] private GameObject _mapObject;
    public UnityEvent OnMapOpened;
    public UnityEvent OnMapClosed;

    private void Awake()
    {
        foreach (Location location in GetComponentsInChildren<Location>())
        {
            locations.Add(location);
        }
    }

    private void OnEnable()
    {
        Location.OnLocationClicked += OnLocationClicked;
    }

    public void ShowMap()
    {
        OnMapOpened?.Invoke();
        
        foreach (Location location in locations)
        {
            bool revealed = location.Revealed;
            location.gameObject.SetActive(revealed);
        }
    }

    public void HideMap()
    {
        
        foreach (Location location in locations)
        {
            location.gameObject.SetActive(false);
        }

        OnMapClosed?.Invoke();
    }

    private void OnLocationClicked(GameObject locationObject)
    {
        foreach (Location location in locations)
        {
            if (location.IsLocation(locationObject))
            {
                location.EnableObject();
            }
            else
            {
                location.DisableObject();
            }
        }

        HideMap();
    }
}