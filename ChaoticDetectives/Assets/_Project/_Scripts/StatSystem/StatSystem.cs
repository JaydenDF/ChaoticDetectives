using System;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    private static StatSystem instance;

    #region Singleton

    public static StatSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StatSystem>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<StatSystem>();
                    singletonObject.name = "StatSystem (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }
    #endregion

    public static Action<Stat[]> OnStatsChanged;
    public static Action<CharacterSO> OnCharacterChanged;

    [SerializeField] private CharacterSO _currentCharacterSO;
    
    [SerializeField]
    private Stat[] stats =
    {
        new Stat { statType = StatType.Perception, value = 0 },
        new Stat { statType = StatType.Creativity, value = 0 }
    };

    private void Awake() {

        if(_currentCharacterSO != null)
        {
            SetStatsFromCharacterSO(_currentCharacterSO);
        }
    }

    public void NewCharacterSO(CharacterSO characterSO)
    {
        SetCurrentCharacterSO(characterSO);
        SetStatsFromCharacterSO(characterSO);
    }

    private void SetCurrentCharacterSO(CharacterSO characterSO)
    {
        _currentCharacterSO = characterSO;
        OnCharacterChanged?.Invoke(characterSO);
    }
    public void SetStatValue(StatType statType, uint value)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].statType == statType)
            {
                stats[i].value = value;
            }
        }

        OnStatsChanged?.Invoke(stats);
    }

    public void SetStatsFromCharacterSO(CharacterSO characterSO)
    {
        foreach (var stat in characterSO.stats)
        {
            SetStatValue(stat.statType, stat.value);
        }
    }

    public void IncreaseStatValue(StatType statType, uint value)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].statType == statType)
            {
                stats[i].value += value;
            }
        }

        OnStatsChanged?.Invoke(stats);
    }

    public bool CheckStatValue(StatType statType, uint value)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].statType == statType)
            {
                return stats[i].value >= value;
            }
        }
        return false;
    }

    public uint GetStatValue(StatType statType)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].statType == statType)
            {
                return stats[i].value;
            }
        }
        return 0;
    }
}

