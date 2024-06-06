using UnityEngine;

public class IncrementStatsFunction : MonoBehaviour {
    public StatType statToIncrement;
    public int amountToIncrement = 1;
    private StatSystem _statSystem;

    private void Awake() {
        _statSystem = StatSystem.Instance;
    }

    public void IncrementStats() {
        _statSystem.IncreaseStatValue(statToIncrement, (uint)amountToIncrement);
    }
}