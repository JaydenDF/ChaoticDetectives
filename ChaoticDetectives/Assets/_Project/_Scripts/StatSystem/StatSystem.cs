using System;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    #region Singleton

    private static StatSystem instance;


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
                }
            }
            return instance;
        }
    }
    #endregion

    public static Action<Stat[]> OnStatsChanged;
    public static Action<CharacterSO> OnCharacterChanged;
    public static Action<Stat> OnStatModfied;

    [SerializeField] private List<CharacterSO> _characters = new List<CharacterSO>();
    private int _currentCharacterIndex = 0;
    private CharacterSO _currentCharacterSO => _characters[_currentCharacterIndex];

    public List<Stat[]> copyOfStats = new List<Stat[]>();

    private void OnEnable() {
        LoopMaster.OnLooped += ReloadStats;
    }
    private void OnDisable()
    {
        ReloadStats();
    }

    private void Start()
    {
        NewCharacterSO(_currentCharacterSO);
        SaveStats();
    }
    private void SaveStats()
    {
        copyOfStats.Clear();
        foreach (var character in _characters)
        {
            Stat[] statsCopy = new Stat[character.stats.Length];
            for (int i = 0; i < character.stats.Length; i++)
            {
                statsCopy[i] = character.stats[i].Clone();
            }
            copyOfStats.Add(statsCopy);
        }
    }

    private void ReloadStats()
    {
        if  (copyOfStats.Count == 0) {return;}

        for (int i = 0; i < _characters.Count; i++)
        {
            Stat[] statsCopy = new Stat[copyOfStats[i].Length];
            for (int j = 0; j < copyOfStats[i].Length; j++)
        {
                statsCopy[j] = copyOfStats[i][j].Clone();
            }
            _characters[i].stats = statsCopy;
        }

        OnStatsChanged?.Invoke(_currentCharacterSO.stats);
    }

    public void NewCharacterSO(CharacterSO characterSO)
    {
        SetCurrentCharacterSO(characterSO);
        SetStatsFromCharacterSO(characterSO);
    }

    private void SetCurrentCharacterSO(CharacterSO characterSO)
    {
        _currentCharacterIndex = _characters.IndexOf(characterSO);
        OnCharacterChanged?.Invoke(characterSO);
    }
    public void SetStatValue(StatType statType, uint value)
    {
        Stat[] currentStats = _currentCharacterSO.stats;
        for (int i = 0; i < currentStats.Length; i++)
        {
            if (currentStats[i].statType == statType)
            {
                currentStats[i].value = value;
            }
        }

        OnStatsChanged?.Invoke(currentStats);
    }

    private void SetStatsFromCharacterSO(CharacterSO characterSO)
    {
        foreach (var stat in characterSO.stats)
        {
            SetStatValue(stat.statType, stat.value);
        }

        Stat[] _currentStats = _currentCharacterSO.stats;
        OnStatsChanged?.Invoke(_currentStats);
    }

    public void IncreaseStatValue(StatType statType, uint value)
    {
        foreach (var character in _characters)
        {
            Stat[] stats = character.stats;
            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i].statType == statType)
                {
                    stats[i].value += value;
                }
            }
        }

        Stat[] currentStats = _currentCharacterSO.stats;

        OnStatsChanged?.Invoke(currentStats);
        OnCharacterChanged?.Invoke(_currentCharacterSO);
        OnStatModfied?.Invoke(currentStats[(int)statType]);
    }

    public bool CheckStatValue(StatType statType, uint value)
    {
        for (int i = 0; i < _currentCharacterSO.stats.Length; i++)
        {
            if (_currentCharacterSO.stats[i].statType == statType)
            {
                if (_currentCharacterSO.stats[i].value >= value)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public uint GetStatValue(StatType statType)
    {
        for (int i = 0; i < _currentCharacterSO.stats.Length; i++)
        {
            if (_currentCharacterSO.stats[i].statType == statType)
            {
                return _currentCharacterSO.stats[i].value;
            }
        }
        return 0;
    }

    public Stat[] GetStats()
    {
        return _currentCharacterSO.stats;
    }
}

