using System;
using System.Collections;
using UnityEngine;

public class SimpleLoop : MonoBehaviour
{
    public static Action OnLooped;
    [SerializeField] private GameObject _locationParent;
    [SerializeField] private GameObject _startLocation;

    public void Loop()
    {
        StartCoroutine(LoopAfter());
    }

    private IEnumerator LoopAfter(float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        OnLooped?.Invoke();

        //DISABLE CHILDREN OF LOCA
        foreach (Transform child in _locationParent.transform)
        {
            child.gameObject.SetActive(false);
        }

        _startLocation.SetActive(true);
    }
}