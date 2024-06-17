using System;
using UnityEngine;

public class UIStats : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _perceptionText;
    [SerializeField] private TMPro.TextMeshProUGUI _creativityText;
    [SerializeField] private TMPro.TextMeshProUGUI _intelligenceText;

    private void OnEnable()
    {
        StatSystem.OnStatsChanged += UpdateUI;
    }

    private void OnDisable()
    {
        StatSystem.OnStatsChanged -= UpdateUI;
    }

    private void UpdateUI(Stat[] obj)
    {
        foreach (Stat stat in obj)
        {
            switch (stat.statType)
            {
                case StatType.Perception:
                    _perceptionText.text = stat.value.ToString();
                    break;
                case StatType.Creativity:
                    _creativityText.text = stat.value.ToString();
                    break;
                case StatType.Intelligence:
                    _intelligenceText.text = stat.value.ToString();
                    break;
            }
        }
    }
}