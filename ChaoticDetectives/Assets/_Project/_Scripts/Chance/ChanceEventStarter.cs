using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Events;

public class ChanceEventStarter : MonoBehaviour, IInteractable
{
    public static EventHandler<ChanceEvent> OnChanceEvent;
    public List<UnityEvent> outcomEvents;

    public ChanceEvent _chanceEvent;
    private SpriteRenderer _spriteRenderer;

    private bool _hasBeenClicked = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnClick()
    {
        if (_hasBeenClicked) return;

        ChanceEvent();
        _hasBeenClicked = true;
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
    protected void ChanceEvent()
    {
        OnChanceEvent?.Invoke(this, _chanceEvent);
    }
    protected void ReactToOutcome(ChanceOutcome outcome)
    {
        _spriteRenderer.sprite = outcome.sprite;
        //get the index of the outcome
        int index = _chanceEvent.GetOutcomeIndex(outcome);
        //invoke the event at the same index if it exists
        if (index < outcomEvents.Count)
        {
            outcomEvents[index].Invoke();
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