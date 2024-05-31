using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Events;

public class ChanceEventStarter : MonoBehaviour, IInteractable
{
    public static EventHandler<ChanceEvent> OnChanceEvent;
    public List<UnityEvent> OutcomeEvents;

    public ChanceEvent _chanceEvent;

    private bool _hasBeenClicked = false;
    public void OnClick()
    {
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

#if UNITY_EDITOR
[CustomEditor(typeof(ChanceEventStarter))]
public class ChanceEventStarterEditor : Editor
{
    private Editor chanceEventEditor;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        ChanceEventStarter chanceEventStarter = (ChanceEventStarter)target;

        if (chanceEventEditor == null && chanceEventStarter._chanceEvent != null)
        {
            chanceEventEditor = CreateEditor(chanceEventStarter._chanceEvent);
        }

        if (chanceEventEditor != null)
        {
            EditorGUI.BeginChangeCheck();
            chanceEventEditor.OnInspectorGUI();
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(chanceEventStarter._chanceEvent);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif