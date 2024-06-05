using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class OnEnabeledStatChecker : MonoBehaviour {
    [SerializeField] private List<Stat> StatsToMatch;
    public UnityEvent OnEnoughStats;
    public UnityEvent OnNotEnoughStats;
    private StatSystem _statSystem;

    private void Start() {
    }

    private void OnEnable() {
        _statSystem = StatSystem.Instance;
        CheckStats();
    }

    private void CheckStats()
    {
        bool enoughStats = true;
        foreach (var stat in StatsToMatch)
        {
            if (_statSystem.GetStatValue(stat.statType) < stat.value)
            {
                enoughStats = false;
                break;
            }
        }

        if (enoughStats)
        {
            OnEnoughStats?.Invoke();
        }
        else
        {
            OnNotEnoughStats?.Invoke();
        }
    }
}