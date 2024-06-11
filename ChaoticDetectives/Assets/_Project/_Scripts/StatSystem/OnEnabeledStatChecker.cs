using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class OnEnabeledStatChecker : MonoBehaviour
{

    [SerializeField] private uint _requiredStats = 0;
    public UnityEvent OnEnoughStats;
    public UnityEvent OnNotEnoughStats;
    private StatSystem _statSystem;

    private void Start()
    {
    }

    private void OnEnable()
    {
        _statSystem = StatSystem.Instance;
        CheckStats();
    }

    private void CheckStats()
    {
        uint totalStats = 0;

        foreach (var stat in _statSystem.GetStats())
        {
            totalStats += stat.value;
        }

        if (totalStats < _requiredStats)
        {
            OnNotEnoughStats.Invoke();
        }
        else
        {
            OnEnoughStats.Invoke();
        }
    }
}