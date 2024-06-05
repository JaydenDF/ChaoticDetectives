using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "ChanceEvent", menuName = "ChanceEvent", order = 0)]
public class ChanceEvent : ScriptableObject
{
    public uint maxRollPossible = 12;
    public uint minRollPossible = 0;
    [Space(10)]

    [Range(0, 12)]
    public uint neededRoll;
    public StatType statTypeCheck;

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

    public int GetOutcomeIndex(ChanceOutcome outcome)
    {
        for (int i = 0; i < _outcomes.Length; i++)
        {
            if (_outcomes[i] == outcome)
            {
                return i;
            }
        }
        return -1;
    }

    public uint GetModifier()
    {
        StatSystem statSystem = StatSystem.Instance;
        uint value = statSystem.GetStatValue(statTypeCheck);
        return value;
    }
}
