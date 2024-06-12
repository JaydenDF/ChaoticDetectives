using UnityEngine;

[System.Serializable]
public struct Stat
{
    public StatType statType;

    [Range(0, 27)]
    public uint value;
    public Stat Clone()
    {
        return new Stat
        {
            statType = statType,
            value = value
        };
    }
}
