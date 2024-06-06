using UnityEngine;

public class IncrementStatsOnItemGathered : MonoBehaviour {
    public GameObject itemToCheck;
    public StatType statToIncrement;
    public int amountToIncrement = 1;
    private StatSystem _statSystem;
    private void Awake() {
        Inventory.OnItemAddedWithGameObject += CheckIfItemIsGathered;
    }

    private void CheckIfItemIsGathered(GameObject otherItemToCheck) {
        if(_statSystem == null) {
            _statSystem = StatSystem.Instance;
        }

        if (otherItemToCheck == this.itemToCheck) {
            _statSystem.IncreaseStatValue(statToIncrement, (uint)amountToIncrement);
        }
    }
}