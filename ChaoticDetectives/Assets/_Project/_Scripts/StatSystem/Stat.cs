using UnityEngine;

[System.Serializable]
public struct Stat
{
    public StatType statType;

    [Range(0, 27)]
    public uint value;
}
