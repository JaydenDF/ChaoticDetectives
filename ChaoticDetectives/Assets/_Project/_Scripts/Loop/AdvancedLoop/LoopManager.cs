using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    private List<LoopContainer> _loops;

    private int _currentLoopIndex = 0;
    private LoopContainer _currentLoop => _loops[_currentLoopIndex];
    private LoopContainer _nextLoop => _loops[_currentLoopIndex + 1];
    private Transform _nextLoopTransform => _loops[_currentLoopIndex + 1].transform;

    private bool _hasLoaded = false;
    private void Awake()
    {
        if (_hasLoaded) return;

        _loops = new List<LoopContainer>(GetComponentsInChildren<LoopContainer>());

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
        if (!_hasLoaded) {Awake();}

        _nextLoop.gameObject.SetActive(true);
        _currentLoop.Loop(_nextLoopTransform);
        _currentLoop.gameObject.SetActive(false);
        _currentLoopIndex = _currentLoopIndex++;
    }

}