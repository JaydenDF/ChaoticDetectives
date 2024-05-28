using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [SerializeField] private List<LoopContainer> _loops;

    private int _currentLoopIndex = 0;
    private LoopContainer _currentLoop => _loops[_currentLoopIndex];
    private LoopContainer _nextLoop => _loops[_currentLoopIndex + 1];
    private Transform _nextLoopTransform => _loops[_currentLoopIndex + 1].transform;
    private void Awake()
    {
        foreach (LoopContainer loop in _loops)
        {
            loop.gameObject.SetActive(false);
        }

        if (_loops.Count > 0)
        {
            _loops[_currentLoopIndex].gameObject.SetActive(true);
        }

    }
    [ContextMenu("Loop")]
    public void Loop()
    {
        _nextLoop.gameObject.SetActive(true);
        _currentLoop.Loop(_nextLoopTransform);
        _currentLoop.gameObject.SetActive(false);
        _currentLoopIndex = _currentLoopIndex++;    
    }

}