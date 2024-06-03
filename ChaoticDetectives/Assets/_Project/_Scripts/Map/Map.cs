using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map : MonoBehaviour
{
    private List<Location> locations = new List<Location>();
    private List<Location> nonUiLocations;
    [SerializeField] private GameObject _mapObject;
    public UnityEvent OnMapOpened;
    public UnityEvent OnMapClosed;

    private bool _isMapOpen = false;
    private void Start()
    {
        Location.OnLocationClicked += OnLocationClicked;
        foreach (Location location in GetComponentsInChildren<Location>())
        {
            locations.Add(location);
        }

        nonUiLocations = new List<Location>(locations);
        Location[] uiLocations = FindObjectsOfType<Location>(true);
        foreach (Location location in uiLocations)
        {
            foreach (Location uiLocation in locations)
            {
                if (location != uiLocation)
                {
                    nonUiLocations.Add(location);
                }
            }
        }
    }

    public void ShowMap()
    {
        OnMapOpened?.Invoke();
        
        foreach (Location location in locations)
        {
            bool revealed = location.Revealed;
            location.gameObject.SetActive(revealed);
        }

        _isMapOpen = true;
    }

    public void HideMap()
    {
        foreach (Location location in locations)
        {
            location.gameObject.SetActive(false);
        }

        _isMapOpen = false;

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

        foreach (Location location in nonUiLocations)
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

        if (_isMapOpen)
        {
            HideMap();
        }
    }
}