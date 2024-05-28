using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "ChanceEvent", menuName = "ChanceEvent", order = 0)]
public class ChanceEvent : ScriptableObject
{
    public uint maxRollPossible = 12;
    public uint minRollPossible = 0;
    [Space(10)]
    [HideInInspector] public uint neededRoll;

    public ChanceOutcome[] _outcomes = new ChanceOutcome[2];

    public ChanceOutcome GetOutcomeFromRoll(uint roll)
    {
        if (roll < neededRoll)
        {
            return _outcomes[0];
        }
        else
        {
            return _outcomes[1];
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(ChanceEvent))]
public class ChanceEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChanceEvent chanceEvent = (ChanceEvent)target;
        base.OnInspectorGUI();
        // Ensure the min is less than or equal to max
        chanceEvent.minRollPossible = (uint)Mathf.Min(chanceEvent.minRollPossible, chanceEvent.maxRollPossible);

        // Ensure the max is greater than or equal to min
        chanceEvent.maxRollPossible = (uint)Mathf.Max(chanceEvent.maxRollPossible, chanceEvent.minRollPossible);

        // Ensure needed roll is within the range
        chanceEvent.neededRoll = (uint)EditorGUILayout.Slider("Needed Roll", chanceEvent.neededRoll, chanceEvent.minRollPossible, chanceEvent.maxRollPossible);
    }
}
#endif