using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Events;

public class ChanceEventStarter : MonoBehaviour, IInteractable
{
    public UnityEvent OnClicked;
    public static EventHandler<ChanceEvent> OnChanceEvent;
    public List<UnityEvent> OutcomeEvents;

    public ChanceEvent _chanceEvent;

    private bool _hasBeenClicked = false;
    public void OnClick()
    {
        OnClicked.Invoke();
        ChanceEvent();
    }

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }

    public void OnUIRollled(uint roll)
    {
        ReactToOutcome(_chanceEvent.GetOutcomeFromRoll(roll));
    }
    public void ExecuteEvent()
    {
        ChanceEvent();
    }
    protected void ChanceEvent()
    {
        if (_hasBeenClicked) return;
        OnChanceEvent?.Invoke(this, _chanceEvent);
        _hasBeenClicked = true;
    }
    protected void ReactToOutcome(ChanceOutcome outcome)
    {
        int index = _chanceEvent.GetOutcomeIndex(outcome);
        if (index < OutcomeEvents.Count)
        {
            OutcomeEvents[index].Invoke();
        }
    }
}
