using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [SerializeField] private List<LoopContainer> _loops;

    private int _currentLoopIndex = 0;
    private LoopContainer _currentLoop => _loops[_currentLoopIndex];

    private bool _hasLoaded = false;
    private void Awake()
    {
        if (_hasLoaded) return;

        _loops = new List<LoopContainer>(GetComponentsInChildren<LoopContainer>(true));

        foreach (LoopContainer loop in _loops)
        {
            loop.gameObject.SetActive(false);
        }

        if (_loops.Count > 0)
        {
            _loops[_currentLoopIndex].gameObject.SetActive(true);
        }

        _hasLoaded = true;
    }
    public void Loop()
    {
        if (!_hasLoaded) { Awake(); }

        if (_currentLoopIndex < _loops.Count - 1)
        {
            LoopContainer nextLoop;
            if (_currentLoopIndex + 1 < _loops.Count)
            {
                nextLoop = _loops[_currentLoopIndex + 1];
            }
            else
            {
                nextLoop = _loops[0];
            }
            nextLoop.gameObject.SetActive(true);
            _currentLoop.Loop(nextLoop.gameObject.transform);
            _currentLoop.gameObject.SetActive(false);
            _currentLoopIndex++;
        }

        Debug.Log("Looped" + _currentLoopIndex + this.name); ;
    }



    public void EnableLoop(int index)
    {
        int value = index - 1;

        if (!_hasLoaded) { Awake(); }

        if (value < _loops.Count)
        {
            _currentLoop.gameObject.SetActive(false);
            _currentLoopIndex = value;
            _currentLoop.gameObject.SetActive(true);
        }
    }
}